using Entities;

namespace Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository<UserEntity> userRepository { get; }
        public Task<int> SaveChangesAsync();
        public void Dispose();
    }
}
