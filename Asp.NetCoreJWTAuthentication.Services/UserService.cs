namespace Asp.NetCoreJWTAuthentication.Services
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;

    using Asp.NetCoreJWTAuthentication.Domain;
    using Asp.NetCoreJWTAuthentication.Models;
    using Asp.NetCoreJWTAuthentication.Shared;

    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration configuration;

        public UserService(
               UserManager<ApplicationUser> userManager,
               SignInManager<ApplicationUser> signInManager,
               IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.userManager = userManager;
        }

        public string BuildToken(string email)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                this.configuration["JwtSettings:Issuer"],
                this.configuration["JwtSettings:Audience"],
                claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<LoginResponse> Login(LoginModel model)
        {
            var user = await this.userManager.FindByEmailAsync(model.Email);
            var response = new LoginResponse();

            if (user == null)
            {
                response.Error = GlobalConstants.InvalidEmailOrPassword;
                response.Succeeded = false;
            }
            else
            {
                response.Succeeded = true;
                response.Token = this.BuildToken(model.Email);
                response.UserEmail = model.Email;
            }

            await this.signInManager.SignInAsync(user, true);
            return response;
        }

        public async Task<RegisterResponse> Register(RegisterModel model)
        {
            var user = new ApplicationUser(model.Email);
            var result = await this.userManager.CreateAsync(user);
            var response = new RegisterResponse();

            if (!result.Succeeded)
            {               
                response.Succeeded = false;

                foreach (var error in result.Errors)
                {
                    response.Errors.Add($"{error.Code}: {error.Description}");
                }
            }
            else
            {
                response.Succeeded = true;
            }

            return response;
        }
    }
}
