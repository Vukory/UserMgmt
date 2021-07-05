using System.ComponentModel.DataAnnotations;

namespace UserMgmt.Models
{
    public enum Gender
    {
        M = 1,
        F = 2,
        Na = 3
    }

    public enum SystemRole 
    { 
        Admin = 1,
        User = 2
    }

    public class AppUser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string LastName { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public SystemRole SystemRole { get; set; }
    }
}