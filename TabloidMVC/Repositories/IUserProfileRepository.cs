using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface IUserProfileRepository
    {
        UserProfile GetByEmail(string email);
        //UserProfile GetByDisplayName(string displayName);
        //UserProfile GetByFirstName(string firstName);
        //UserProfile GetByLastName(string lastName);
        void RegisterUser(UserProfile userProfile);

    }
}