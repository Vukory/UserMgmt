using System;
using System.Collections.Generic;
using System.Linq;
using UserMgmt.Models;

namespace UserMgmt.Data
{
    public class SqlUserMgmtRepo : IUserMgmtRepo
    {
        private readonly ApplicationDbContext _context;

        public SqlUserMgmtRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateAppUser(AppUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.AppUsers.Add(user);
        }

        public void DeleteAppUser(AppUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            
            _context.AppUsers.Remove(user);
        }

        public IEnumerable<AppUser> GetAllAppUsers()
        {
            return _context.AppUsers.ToList();
        }

        public AppUser GetAppUserById(int id)
        {
            return _context.AppUsers.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateAppUser(AppUser user)
        {
            //Nothing
        }
    }
}