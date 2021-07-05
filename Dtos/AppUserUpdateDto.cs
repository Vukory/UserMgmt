using System.ComponentModel.DataAnnotations;
using UserMgmt.Models;

namespace UserMgmt.Dtos
{
    public class AppUserUpdateDto
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(250)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string LastName { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        [MinLength(8)]
        public string PasswordHash { get; set; }

    }
}