using System.Text.Json.Serialization;

namespace Application.Dtos.Account
{
    public class AuthResponse
    {
        public string? Message { get; set; }

        //by default false
        public bool ISAuthenticated { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public List<string>? Roles { get; set; }

        public string? Token { get; set; }

        public DateTime? TokenExpiresOn { get; set; }

        [JsonIgnore]
        public string? RefreshToken { get; set; }

        public DateTime RefreshTokenExpiration { get; set; }
    }
}