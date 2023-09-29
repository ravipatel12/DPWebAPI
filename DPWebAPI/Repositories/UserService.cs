using DPWebAPI.DBContexts;
using DPWebAPI.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
namespace DPWebAPI.Repositories
{
    public class UserService : IUserDetails
    {
        private readonly ApplicationDBContext _dbContext;

        public UserService(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

       

        public async Task<List<Common.UserDetails>> GetUserListAsync()
        {
            return await _dbContext.UserDetail.FromSqlRaw<Common.UserDetails>("GetLoginDetails_Test")
                .ToListAsync();
        }
        public async Task<IEnumerable<Common.UserDetails>> GetUserByIdAsync(int UserId)
        {
            var param = new SqlParameter("@UserId", UserId);

            var UserDetails = await Task.Run(() => _dbContext.UserDetail
                            .FromSqlRaw(@"exec GetPrductByID @ProductId", param).ToListAsync());

            return UserDetails;
        }

        public async Task<int> AddUserAsync(Common.UserDetails user)
        {
            var parameter = new List<SqlParameter>();
            
            parameter.Add(new SqlParameter("@UserName", user.UserName));
            parameter.Add(new SqlParameter("@Password", user.Password));
            

            var result = await Task.Run(() => _dbContext.Database
           .ExecuteSqlRawAsync(@"exec AddNewUser @UserName, @Password", parameter.ToArray()));

            return result;
        }
    }
}
