using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentitySample.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity>
       GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager
                .CreateIdentityAsync(this,
                    DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }

        public string Address { get; set; }
        public string City { get; set; }
        public string County { get; set; }

        // Use a sensible display name for views:
        [Display(Name = "Post Code")]
        public string PostCode { get; set; }

        // Concatenate the address info for display in tables and such:
        public string DisplayAddress
        {
            get
            {
                string dspAddress =
                    string.IsNullOrWhiteSpace(this.Address) ? "" : this.Address;
                string dspCity =
                    string.IsNullOrWhiteSpace(this.City) ? "" : this.City;
                string dspState =
                    string.IsNullOrWhiteSpace(this.County) ? "" : this.County;
                string dspPostalCode =
                    string.IsNullOrWhiteSpace(this.PostCode) ? "" : this.PostCode;

                return string
                    .Format("{0} {1} {2} {3}", dspAddress, dspCity, dspState, dspPostalCode);
            }
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        static ApplicationDbContext()
        {
            // Set the database intializer which is run once during application start
            // This seeds the database with admin user credentials and admin role
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}