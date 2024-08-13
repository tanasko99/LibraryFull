using AutoMapper;
using FluentResults;
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
        public void UpdateAuthor(int id,AuthorUpdateDto author)
        {
            var existingAuthor = _repository.Author.GetAuthorById(id);
            if(existingAuthor == null)
            {
                throw new ArgumentException(
                    "Author with that ID doesn't exist",nameof(id));
            }
            existingAuthor.FirstName = author.FirstName;
            existingAuthor.LastName = author.LastName;
            _repository.Author.UpdateAuthor(existingAuthor);
            _repository.Save();
        }

        public bool DeleteAuthor(int id)
        {
            return _repository.Author.DeleteAuthor(id);
        }

        public Result<AuthorDto> GetAuthorById(int id)
        {
            var author = _repository.Author.GetAuthorById(id);
            if (author == null)
                return Result.Fail("Author not found");
            return Result.Ok(_mapper.Map<AuthorDto>(author));
        }
        
        public IList<AuthorDto> GetAllAuthors()
        {
            var authors = _repository.Author.GetAllAuthors();
            return _mapper.Map<List<AuthorDto>>(authors);
        }

    }
}
