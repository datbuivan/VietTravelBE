using MailKit.Search;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using VietTravelBE.Core.Interface;
using VietTravelBE.Helpers;
using VietTravelBE.Infrastructure.Data.Entities;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace VietTravelBE.Infrastructure.Services
{
    public class VnPayService : IVnPayService
    {
        private readonly IPaymentRepository _paymentRepo;
        private readonly IBookingRepository _bookingRepo;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unit;
        private readonly ILogger<VnPayService> _logger;
        public VnPayService(IPaymentRepository paymentRepo, IBookingRepository bookingRepo, IConfiguration configuration, IUnitOfWork unit, ILogger<VnPayService> logger)
        {
            _paymentRepo = paymentRepo;
            _bookingRepo = bookingRepo;
            _configuration = configuration;
            _unit = unit;
            _logger = logger;
        }

        public async Task<string> CreateVNPayPaymentUrlAsync(int bookingId)
        {
            var booking = await _bookingRepo.GetByIdAsync(bookingId);
            if (booking == null)
            {
                throw new ArgumentException("Booking not found.");
            }

            var payment = new Payment
            {
                BookingId = bookingId,
                Amount = booking.TotalPrice,
                PaymentMethod = "VNPay",
                PaymentDate = DateTime.UtcNow,
                Status = "Pending",
                TransactionId = "",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _paymentRepo.Add(payment);
            await _unit.Complete();

            var tmnCode = _configuration["VNPay:TmnCode"];
            var hashSecret = _configuration["VNPay:HashSecret"];
            var paymentUrl = _configuration["VNPay:PaymentUrl"];
            var returnUrl = _configuration["VNPay:ReturnUrl"];
            var version = _configuration["VNPay:Version"];
            var command = _configuration["VNPay:Command"];
            var currCode = _configuration["VNPay:CurrCode"];
            var locale = _configuration["VNPay:Locale"];
            var clientIp = _configuration["VNPay:ClientIp"];
            var orderId = $"BOOKING_{booking.Id}_{DateTime.Now.Ticks}";

            if (string.IsNullOrEmpty(tmnCode) || string.IsNullOrEmpty(hashSecret) || 
                string.IsNullOrEmpty(paymentUrl) || string.IsNullOrEmpty(returnUrl) ||
                string.IsNullOrEmpty(version) || string.IsNullOrEmpty(command) ||
                string.IsNullOrEmpty(currCode) || string.IsNullOrEmpty(locale) || string.IsNullOrEmpty(clientIp) )
            {
                throw new InvalidOperationException("VNPay configuration is missing.");
            }

            var vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(_configuration["TimeZoneId"]!);
            var vnNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vnTimeZone);
            var expireDate = vnNow.AddMinutes(15);

            var vnpay = new VnPay();

            vnpay.AddRequestData("vnp_Version", version);
            vnpay.AddRequestData("vnp_Command", command);
            vnpay.AddRequestData("vnp_TmnCode", tmnCode);
            vnpay.AddRequestData("vnp_Amount", ((long)(payment.Amount * 100)).ToString());
            vnpay.AddRequestData("vnp_CreateDate", vnNow.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_ExpireDate", expireDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", currCode);
            vnpay.AddRequestData("vnp_IpAddr", clientIp);
            vnpay.AddRequestData("vnp_Locale", locale);
            vnpay.AddRequestData("vnp_OrderInfo", $"Thanh toan Booking {bookingId}");
            vnpay.AddRequestData("vnp_OrderType", "other");
            vnpay.AddRequestData("vnp_ReturnUrl", returnUrl);
            //vnpay.AddRequestData("vnp_IpnUrl", string.IsNullOrEmpty(ipnUrl) ? returnUrl : ipnUrl);
            vnpay.AddRequestData("vnp_TxnRef", orderId);

            var paymentUrlResult = vnpay.CreateRequestUrl(paymentUrl, hashSecret);

            return paymentUrlResult;
        }

        public async Task<(bool Success, string Message, object Data)> ProcessVnPayReturnAsync(IQueryCollection query)
        {
            var vnpay = new VnPay();
            var hashSecret = _configuration["VNPay:HashSecret"];

            foreach (var key in query.Keys)
            {
                vnpay.AddResponseData(key, query[key]);
            }

            string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
            string vnp_TxnRef = vnpay.GetResponseData("vnp_TxnRef");
            string vnp_SecureHash = vnpay.GetResponseData("vnp_SecureHash");
            string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
            string vnp_Amount = vnpay.GetResponseData("vnp_Amount");

            if (string.IsNullOrEmpty(hashSecret))
            {
                throw new InvalidOperationException("VNPay configuration is missing.");
            }

            bool isValidSignature = vnpay.ValidateSignature(vnp_SecureHash, hashSecret);
            if (!isValidSignature)
            {
                return (false, "Chữ ký không hợp lệ. Thanh toán có thể bị thay đổi.", new Payment());
            }

            var txnRefParts = vnp_TxnRef.Split('_');
            if (txnRefParts.Length < 2)
            {
                return (false, "Mã giao dịch không hợp lệ.", new Payment());
            }

            int bookingId;
            if (!int.TryParse(txnRefParts[1], out bookingId))
            {
                return (false, "Không thể phân tích mã giao dịch.", new Payment());
            }

            var payment = await _paymentRepo.GetByBookingIdAsync(bookingId);
            if (payment == null)
            {
                return (false, "Không tìm thấy thông tin thanh toán.", new Payment());
            }

            if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
            {
                _logger.LogInformation("VNPAY callback received. BookingId = {0}", bookingId);
                _logger.LogInformation("Payment Status Before: {0}", payment.Status);
                payment.Status = "Completed";
                payment.PaymentDate = DateTime.UtcNow;
                payment.TransactionId = vnpay.GetResponseData("vnp_TransactionNo");

                //_paymentRepo.Update(payment);
                await _unit.Complete();
                _logger.LogInformation("Payment Status After: {0}", payment.Status);

                return (true, "Thanh toán thành công.", new
                {
                    TransactionId = payment.TransactionId,
                    Amount = decimal.Parse(vnp_Amount) / 100 // Chuyển về đơn vị VND
                });
            }
            else
            {
                payment.Status = "Failed";
                _paymentRepo.Update(payment);
                await _unit.Complete();

                return (false, "Thanh toán thất bại.", new
                {
                    ResponseCode = vnp_ResponseCode
                });
            }
        }


        //public async Task<bool> HandleVNPayCallbackAsync(IQueryCollection query)
        //{
        //    var secureHash = query["vnp_SecureHash"];
        //    var vnpayParams = query.Keys
        //        .Where(k => k != "vnp_SecureHash")
        //        .OrderBy(k => k)
        //        .ToDictionary(k => k, k => query[k].ToString());

        //    var signData = string.Join("&", vnpayParams.Select(kvp => $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));
        //    var hashSecret = _configuration["VNPay:HashSecret"];
        //    if (string.IsNullOrEmpty(hashSecret))
        //    {
        //        throw new InvalidOperationException("HashSecret is not configured in appsettings.");
        //    }
        //    var hmacsha512 = new HMACSHA512(Encoding.UTF8.GetBytes(hashSecret));
        //    var computedHash = BitConverter.ToString(hmacsha512.ComputeHash(Encoding.UTF8.GetBytes(signData))).Replace("-", "").ToLower();

        //    if (secureHash != computedHash)
        //    {
        //        return false; 
        //    }

        //    if (!int.TryParse(query["vnp_TxnRef"], out var paymentId))
        //    {
        //        throw new InvalidOperationException("Invalid or missing vnp_TxnRef in query.");
        //    }

        //    var payment = await _paymentRepo.GetByIdAsync(paymentId);
        //    if (payment == null)
        //    {
        //        return false; 
        //    }

        //    var responseCode = query["vnp_ResponseCode"];
        //    payment.Status = responseCode == "00" ? "Completed" : "Failed";
        //    _paymentRepo.Update(payment);

        //    return responseCode == "00";
        //}

    }
}
