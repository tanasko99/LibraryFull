using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace FullLibrary.Security
{
    public class AuthenticationContext : IdentityDbContext<ApplicationUser>
    {

        public AuthenticationContext(DbContextOptions<AuthenticationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            Seed(builder);
        }
        private void Seed(ModelBuilder modelBuilder)
        {
            var adminRoleId = Guid.NewGuid();
            var userRoleId = Guid.NewGuid();
            var librarianRoleId = Guid.NewGuid();
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = adminRoleId.ToString(), Name = Security.UserRoles.Admin, NormalizedName = Security.UserRoles.Admin.ToUpper() },
                new IdentityRole { Id = librarianRoleId.ToString(), Name = Security.UserRoles.Librarian, NormalizedName = Security.UserRoles.Librarian.ToUpper() },
                new IdentityRole { Id = userRoleId.ToString(), Name = Security.UserRoles.User, NormalizedName = Security.UserRoles.User.ToUpper() }
            );

            var hasher = new PasswordHasher<IdentityUser>();

            var adminId = Guid.NewGuid();
            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = adminId.ToString(),
                    UserName = "vuk@admin.com",
                    Email = "vuk@admin.com",
                    NormalizedUserName = "VUK@ADMIN.COM",
                    NormalizedEmail = "VUK@ADMIN.COM",
                    PasswordHash = hasher.HashPassword(null, "passwordD1!")
                }
            );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId.ToString(),
                    UserId = adminId.ToString(),
                }
            );
        }
    }
}
