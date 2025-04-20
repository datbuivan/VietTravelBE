namespace VietTravelBE.Errors
{
    public class ApiException : ApiResponse<Object> 
    {
        public ApiException(int statusCode, string? message = null, string? details = null)
            : base(statusCode, message)
        {
            Details = details;
        }

        public string? Details { get; set; }
    }
}
