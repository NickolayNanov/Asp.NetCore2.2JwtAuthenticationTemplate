namespace Asp.NetCoreJWTAuthentication.Data
{
    using Asp.NetCoreJWTAuthentication.Domain;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions options)
            :base(options)
        {
        }
    }
}
