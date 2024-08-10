namespace FullLibrary.Repository
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        IAuthorRepository Author { get; }
        void Save();
    }
}
