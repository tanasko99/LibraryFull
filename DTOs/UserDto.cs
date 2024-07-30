using System.ComponentModel.DataAnnotations;

namespace FullLibrary.DTOs
{
    public class UserDto
    {
        [Required]
        public string? Email {  get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }
    }
}
