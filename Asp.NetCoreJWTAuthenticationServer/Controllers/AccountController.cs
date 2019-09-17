namespace Asp.NetCoreJWTAuthenticationServer.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Asp.NetCoreJWTAuthentication.Models;
    using Asp.NetCoreJWTAuthentication.Services;

    public class AccountController : ApiController
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginModel loginModel)
        {
            LoginResponse result = await this.userService.Login(loginModel);

            if (!result.Succeeded)
            {
                return this.BadRequest(result.Error);
            }

            return this.Ok(result.Token);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<LoginResponse>> Register([FromBody] RegisterModel registerModel)
        {
            RegisterResponse result = await this.userService.Register(registerModel);

            if (!result.Succeeded)
            {
                var errors = string.Join("\n", result.Errors);
                return this.BadRequest(errors);
            }

            return this.Ok();
        }
    }
}
