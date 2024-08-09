using FullLibrary.Models;

namespace FullLibrary.Repository
{
    public interface IUserRepository
    {
        void CreateUser(User user);
        void UpdateUser(User user);
        User GetUserById(int id);
    }
}
