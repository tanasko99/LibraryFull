using FluentResults;
using FullLibrary.DTOs;


namespace FullLibrary.Services
{
    public interface IBookService
    {
        void CreateBook(BookDto book);
        void UpdateBook(int id, BookDto book);
        IList<BookDto> GetAllBooks();
        Result<BookDto> GetBookById(int id);
        bool DeleteBook(int id);
    }
}
