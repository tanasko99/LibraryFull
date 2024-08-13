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

        public void UpdateAuthor(Author author)
        {
            Update(author);
        }

        public bool DeleteAuthor(int id)
        {
            var author = GetAuthorById(id);
            if (author == null)
            {
                return false;
            }
            Delete(author);
            return true;
        }

        public IQueryable<Author> GetAllAuthors()
        {
            return FindAll();
        }

        public Author GetAuthorById(int id)
        {
            return FindByCondition(x => x.Id == id).FirstOrDefault();
        }

        
    }
}
