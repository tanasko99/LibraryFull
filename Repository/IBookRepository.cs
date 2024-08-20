using FullLibrary.Models;

namespace FullLibrary.Repository
{
    public interface IBookRepository
    {
        IQueryable<Book> GetAllBooks();
        void CreateBook(Book book);
        void UpdateBook(Book book);
        Book GetBookById(int id);
        bool DeleteBook(int id);
    }
}
