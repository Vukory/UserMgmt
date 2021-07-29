using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

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

    /// https://docs.microsoft.com/en-us/aspnet/core/security/authentication/customize-identity-model?view=aspnetcore-5.0#change-the-primary-key-type
    /// https://docs.microsoft.com/en-us/aspnet/core/security/authentication/customize-identity-model?view=aspnetcore-5.0#custom-user-data
    public class AppUser : IdentityUser<int>
    {

        [Required]
        [MaxLength(250)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string LastName { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public SystemRole SystemRole { get; set; }
    }
}