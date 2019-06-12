using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BTA.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
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
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<POI> POIs { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Traveler> Travelers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Category>()
            //    .HasMany(e => e.Comments)
            //    .WithOptional(e => e.Category1)
            //    .HasForeignKey(e => e.category);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.POIs)
                .WithOptional(e => e.Category1)
                .HasForeignKey(e => e.category);

            modelBuilder.Entity<City>()
                .HasMany(e => e.POIs)
                .WithOptional(e => e.City1)
                .HasForeignKey(e => e.city);

            //modelBuilder.Entity<Comment>()
            //    .HasMany(e => e.Comment1)
            //    .WithOptional(e => e.Comment2)
            //    .HasForeignKey(e => e.parentId);

            modelBuilder.Entity<Module>()
                .HasMany(e => e.Categories)
                .WithOptional(e => e.Module1)
                .HasForeignKey(e => e.module);

            //modelBuilder.Entity<POI>()
            //    .HasMany(e => e.Comments)
            //    .WithOptional(e => e.POI1)
            //    .HasForeignKey(e => e.poi);

            modelBuilder.Entity<Traveler>()
                .Property(e => e.fullName)
                .IsFixedLength();

            modelBuilder.Entity<Traveler>()
                .HasMany(e => e.Comments)
                .WithOptional(e => e.Traveler1)
                .HasForeignKey(e => e.traveler);

            base.OnModelCreating(modelBuilder);
        }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public class ApplicationRole : IdentityRole
        {
            public ApplicationRole() : base() { }
            public ApplicationRole(string roleName) : base(roleName) { }
        }
    }
}