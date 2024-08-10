using System.ComponentModel.DataAnnotations;

namespace FullLibrary.DTOs
{
    public class AuthorDto
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }
    }
}
