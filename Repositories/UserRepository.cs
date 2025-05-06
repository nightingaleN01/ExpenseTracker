using Entities;
using Interfaces;
using Dapper;
using DatabaseContext;

namespace Repositories
{
    public class UserRepository : IUserRepository<UserEntity>
    {
        private readonly DBContext _dbContext;

        private const string insertUsersQuery     = @"INSERT INTO dbo.[User] (UserName,Email,Password,CreatedBy,IsDeleted,CreatedDate,LastUpdatedBy,LastUpdatedDate,Version) 
                                                    VALUES (@username , @email , @password , null, 0,GETDATE(),null,GETDATE(),1)

                                                     DECLARE @newUserID INT = SCOPE_IDENTITY()
                                                    UPDATE dbo.[User] set 
                                                    CreatedBy = @newUserID, LastUpdatedBy =@newUserID where UserId=@newUserID";
        private const string deleteUsersQuery     = @"UPDATE dbo.[User]
                                                    SET
                                                    IsDeleted = 1
                                                    WHERE UserId = @userId";
        private const string selectUsersQuery     = @"SELECT * FROM dbo.[User]";
        private const string selectUsersByIdQuery = @"SELECT * FROM dbo.[User] where UserId= @userId";

        private const string updateusersQuery     = @"UPDATE dbo.[User]
                                                      SET
                                                        UserName = UserName,
                                                        Email =  Email,
                                                        Password = Password,
                                                        LastUpdatedBy = @userId,
                                                        LastUpdatedDate = GETDATE()
                                                      WHERE UserId = @userId";
        public UserRepository(DBContext dBContext)
        {
            _dbContext = dBContext;
        }
        public async Task<int> AddUser(UserEntity userEntity)
        {
            var connection = _dbContext.CreateConnection();
            var rowsInserted =  await connection.ExecuteAsync(insertUsersQuery, userEntity);
            return rowsInserted;
        }
        public async Task<IEnumerable<UserEntity>> GetAllUsers()
        {
            var connection = _dbContext.CreateConnection();
            return await connection.QueryAsync<UserEntity>(selectUsersQuery);
        }

        public async Task<UserEntity> GetUserById(int id)
        {
            var connection = _dbContext.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<UserEntity>(selectUsersByIdQuery , new { UserId = id});
        }
        
        public async Task<bool> UpdateUser(int id ,UserEntity entity)
        {
            var connection = _dbContext.CreateConnection();
            var updatedUsers = await connection.ExecuteAsync(updateusersQuery, entity);
            return updatedUsers > 0;
        }

        public async Task<bool> DeleteUser(int id)
        {

            var connection = _dbContext.CreateConnection();
            var rowsAffected = await connection.ExecuteAsync(deleteUsersQuery, new { UserId = id });
            return rowsAffected > 0;
        }
    }
}
