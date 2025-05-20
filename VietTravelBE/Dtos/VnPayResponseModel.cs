namespace VietTravelBE.Dtos
{
    public class PaymentResponseModel
    {
        public string BookingDescription { get; set; }
        public string PaymentMethod { get; set; }
        public bool Success { get; set; }
        public int BookingId { get; set; }
        public DateTime PaymentTime { get; set; }
        public string TransactionId { get; set; }
        public string VnPayResponseCode { get; set; }
    }
}
