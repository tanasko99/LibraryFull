using FullLibrary.DTOs;

namespace FullLibrary.Services
{
    public interface IUserService
    {
        void CreateUser(UserDto user);
        void UpdateUser(int id,UpdateProfileDto model);
    }
}
