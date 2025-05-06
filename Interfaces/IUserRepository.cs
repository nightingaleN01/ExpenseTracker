namespace Interfaces
{
    public interface IUserRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAllUsers();
        public Task<T> GetUserById(int id);
        public Task<int> AddUser(T entity);
        public Task<bool> UpdateUser(int id, T entity);
        public Task<bool> DeleteUser(int id);
    }
}
