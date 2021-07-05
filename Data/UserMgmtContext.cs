using Microsoft.EntityFrameworkCore;
using UserMgmt.Models;

namespace UserMgmt.Data
{
    public class UserMgmtContext : DbContext
    {
        public UserMgmtContext(DbContextOptions<UserMgmtContext> opt) : base(opt)
        {
            
        }

        public DbSet<AppUser> AppUsers { get; set; }
    }
}