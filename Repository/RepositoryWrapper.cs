using FullLibrary.Data;

namespace FullLibrary.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private DataContext _context;
        private IUserRepository _user;
        private IAuthorRepository _author;
        private IBookRepository _book;

        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_context);
                }
                return _user;
            }
        }

        public IAuthorRepository Author
        {
            get
            {
                if (_author == null)
                {
                    _author = new AuthorRepository(_context);
                }
                return _author;
            }
        }
        public IBookRepository Book
        {
            get
            {
                if( _book == null)
                {
                    _book = new BookRepository(_context);
                }
                return _book;
            }
        }

        public RepositoryWrapper(DataContext context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
