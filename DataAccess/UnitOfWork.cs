using Entities;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories;
using DatabaseContext;
namespace DataAccess
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly IDbConnection _connection;       // Database connection
        private readonly IDbTransaction _transaction;     // Transaction object
        public IUserRepository<UserEntity> userRepository { get; }
        public UnitOfWork(DBContext dBContext) 
        {
            _connection = dBContext.CreateConnection();
            _connection.Open();
            _transaction = _connection.BeginTransaction();
            userRepository = new UserRepository(dBContext);
        }

        public async Task<int> SaveChangesAsync()
        {

            try
            {
                _transaction.Commit();
                return 1;
            }
            catch
            {
                _transaction.Rollback();
                return 0;
            }
            finally
            {
                _transaction.Dispose();
                _connection.Close();
            }

        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Dispose();
        }   

    }
}
