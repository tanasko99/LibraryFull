using Microsoft.AspNetCore.Identity;

namespace FullLibrary.Security
{
    public class ApplicationUser : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    } 
}
