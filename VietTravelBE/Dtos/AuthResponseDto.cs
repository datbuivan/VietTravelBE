namespace VietTravelBE.Dtos
{
    public class AuthResponseDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string UserName { get; set; }
        public IList<string> Roles { get; set; }
    }
}
