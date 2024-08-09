using FullLibrary.Data;
using FullLibrary.Models;

namespace FullLibrary.Repository
{
    public class UserRepository : RepositoryBase<User>,IUserRepository
    {
        public UserRepository(DataContext context) : base(context) { }

        public void CreateUser(User user)
        {
            Create(user);
        }
        public User GetUserById(int id)
        {
            return FindByCondition(x => x.Id == id).FirstOrDefault();
        }

        public void UpdateUser(User user)
        {
            Update(user);
        }
    }
}
