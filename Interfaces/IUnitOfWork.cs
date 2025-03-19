namespace Interfaces
{
    public interface IUnitOfWork
    {
        //IUserRepository userRepository;
        Task<int> CommitAsync();
    }
}
