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
            throw new NotImplementedException();
        }
    }
}
