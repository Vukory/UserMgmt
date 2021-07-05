using System.Collections.Generic;
using UserMgmt.Models;

namespace UserMgmt.Data
{
    public interface IUserMgmtRepo
    {
        bool SaveChanges();
        IEnumerable<AppUser> GetAllAppUsers();
        AppUser GetAppUserById(int id);
        void CreateAppUser(AppUser user);
        void UpdateAppUser(AppUser user);
        void DeleteAppUser(AppUser user);

        

    }
}
