using FullLibrary.Data;
using FullLibrary.Models;

namespace FullLibrary.Repository
{
    public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
    {
        public AuthorRepository(DataContext context) : base(context) { }

        public void CreateAuthor(Author author)
        {
            Create(author);
        }
    }
}
