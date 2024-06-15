namespace Project_Keystone.Api.Models.DTOs
{
    public class TokenModel
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public string? Message  { get; set; }
    }
}
