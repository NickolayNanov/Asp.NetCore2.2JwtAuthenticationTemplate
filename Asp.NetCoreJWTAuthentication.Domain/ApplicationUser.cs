namespace Asp.NetCoreJWTAuthentication.Domain
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser<string>
    {
        public ApplicationUser(string email)
        {
            this.Email = this.UserName = email;
        }
    }
}
