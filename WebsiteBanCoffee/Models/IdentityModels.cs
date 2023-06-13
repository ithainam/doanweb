using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebsiteBanCoffee.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Full Name"), Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public bool Gender { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("CoffeeShopDbContext", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetails> Details { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        
    }
}