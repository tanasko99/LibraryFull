using FullLibrary.Data;
using FullLibrary.Models;

namespace FullLibrary.Repository
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(DataContext context) : base(context) { }

        public void CreateBook(Book book)
        {
            Create(book);
        }
        public void UpdateBook(Book book)
        {
            Update(book);
        }
        public bool DeleteBook(int id)
        {
            var book = GetBookById(id);
            if (book == null)
            {
                return false;
            }
            Delete(book);
            return true;
        }
        public Book GetBookById(int id)
        {
            return FindByCondition(x => x.Id == id).FirstOrDefault();
        }
        public IQueryable<Book> GetAllBooks()
        {
            return FindAll();
        }
    }
}
