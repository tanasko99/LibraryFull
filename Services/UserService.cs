using AutoMapper;
using FullLibrary.DTOs;
using FullLibrary.Models;
using FullLibrary.Repository;

namespace FullLibrary.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repository;
        public UserService(IMapper mapper, IRepositoryWrapper repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
         public void CreateUser(UserDto user)
         {
            _repository.User.CreateUser(_mapper.Map<User>(user));
            _repository.Save();
         }

        public void UpdateUser(int id, UpdateProfileDto model)
        {
            var existingUser = _repository.User.GetUserById(id);
            if (existingUser == null)
            {
                throw new ArgumentException(
                    "User with that ID doesn't exist", nameof(id));
            }
            existingUser.FirstName = model.FirstName;
            existingUser.LastName = model.LastName;
            _repository.User.UpdateUser(existingUser);
            _repository.Save();
        }
    }
}
