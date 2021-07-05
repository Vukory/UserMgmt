using UserMgmt.Models;

namespace UserMgmt.Dtos
{
    public class AppUserReadDto
    {
        
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }
        
        public string Username { get; set; }

        public SystemRole SystemRole { get; set; }
    }
}