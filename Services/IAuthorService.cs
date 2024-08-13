using FluentResults;
using FullLibrary.DTOs;

namespace FullLibrary.Services
{
    public interface IAuthorService
    {
        void CreateAuthor(AuthorDto author);
        void UpdateAuthor(int id,AuthorUpdateDto author);
        IList<AuthorDto> GetAllAuthors();
        Result<AuthorDto> GetAuthorById(int id);
        bool DeleteAuthor(int id);
    }
}
