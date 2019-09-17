namespace Asp.NetCoreJWTAuthentication.Services
{
    using System.Threading.Tasks;

    using Asp.NetCoreJWTAuthentication.Models;

    public interface IUserService
    {
        string BuildToken(string email);

        Task<LoginResponse> Login(LoginModel model);

        Task<RegisterResponse> Register(RegisterModel model);
    }
}
