using AutoMapper;
using FluentResults;
using FullLibrary.DTOs;
using FullLibrary.Models;
using FullLibrary.Repository;

namespace FullLibrary.Services
{
    public class BookService : IBookService  
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repository;

        public BookService(IMapper mapper, IRepositoryWrapper repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public void CreateBook(BookDto book)
        {
            _repository.Book.CreateBook(_mapper.Map<Book>(book));
            _repository.Save();
        }
        public void UpdateBook(int id,BookDto book)
        {
            var existingBook = _repository.Book.GetBookById(id);
            if (existingBook == null)
            {
                throw new ArgumentException(
                    "Book with that ID doesn't exist", nameof(id));
            }
            existingBook.Title = book.Title;
            _repository.Book.UpdateBook(existingBook);
            _repository.Save();
        }
        public bool DeleteBook(int id)
        {
            return _repository.Book.DeleteBook(id);
        }
        public Result<BookDto> GetBookById(int id)
        {
            var book = _repository.Book.GetBookById(id);
            if (book == null)
                return Result.Fail("Book not found");
            return Result.Ok(_mapper.Map<BookDto>(book));
        }
        public IList<BookDto> GetAllBooks()
        {
            var books = _repository.Book.GetAllBooks();
            return _mapper.Map<List<BookDto>>(books);
        }
    }
}
