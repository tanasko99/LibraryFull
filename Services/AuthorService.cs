using AutoMapper;
using FullLibrary.DTOs;
using FullLibrary.Models;
using FullLibrary.Repository;

namespace FullLibrary.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repository;
        public AuthorService(IMapper mapper, IRepositoryWrapper repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public void CreateAuthor(AuthorDto author)
        {
            _repository.Author.CreateAuthor(_mapper.Map<Author>(author));
            _repository.Save();
        }
    }
}
