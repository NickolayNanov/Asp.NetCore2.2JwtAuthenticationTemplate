namespace Asp.NetCoreJWTAuthentication.Models
{
    public class LoginResponse
    {
        public string Token { get; set; }

        public bool Succeeded { get; set; }

        public string Error { get; set; }

        public string UserEmail { get; set; }
    }
}
