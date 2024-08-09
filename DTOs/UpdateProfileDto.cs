using System.ComponentModel.DataAnnotations;

namespace FullLibrary.DTOs
{
    public class UpdateProfileDto
    {
        public string? Email { get; set; }


        [Required]
        [StringLength(100)]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string? LastName { get; set; }
    }
}
