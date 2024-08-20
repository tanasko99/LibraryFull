namespace FullLibrary.Repository
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        IAuthorRepository Author { get; }
        IBookRepository Book { get; }
        void Save();
    }
}
