using FullLibrary.Models;

namespace FullLibrary.Repository
{
    public interface IAuthorRepository
    {
        void CreateAuthor(Author author);
        void UpdateAuthor(Author author);
        bool DeleteAuthor(int id);
        Author GetAuthorById(int id);
        IQueryable<Author> GetAllAuthors();
    }
}
