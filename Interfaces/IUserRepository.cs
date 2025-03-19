namespace Interfaces
{
    public class IUserRepository
    {
        Task<IEnumerable<T>> GetAllUsers();
        Task<T> GetUserById(int id);
        Task<int> AddUser(T entity);
        Task<bool> UpdateUser(T entity);
        Task<bool> DeleteUser(int id);
    }
}
