using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RWADatabaseLibrary.Models;

namespace Javno.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser<int, RwaUserLogin, RwaUserRole, RwaUserClaim>
    {
        public string Address { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser,int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, RwaRole, int, RwaUserLogin, RwaUserRole, RwaUserClaim>
    {
        public ApplicationDbContext()
            : base("apartments")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }


    public class RwaRole : IdentityRole<int, RwaUserRole>
    {
        public RwaRole() { }
        public RwaRole(string name) { Name = name; }
    }

    public class RwaUserRole : IdentityUserRole<int> { }

    public class RwaUserClaim : IdentityUserClaim<int> { }

    public class RwaUserLogin : IdentityUserLogin<int> { }
}