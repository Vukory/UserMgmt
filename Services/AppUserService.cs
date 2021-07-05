namespace UserMgmt.Services
{
    public class AppUserService
    {
        public string GetAvailableUsername(string firstName, string lastName)
        {
            return firstName[0] + lastName;
        }
    }
}