using DPWebAPI.DBContexts;
using DPWebAPI.Entities;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace DPWebAPI.Repositories
{

    public class LoginDetails : ILoginDetails
    {
        private readonly ApplicationDBContext _dbContext;
        
        public LoginDetails(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Common.LoginDetails>> GetLoginDetailsAsync(string UserName, string PassWord)
        {
            
            
                var param = new List<SqlParameter>();
                param.Add(new SqlParameter("@UserName", UserName));
                param.Add(new SqlParameter("@Password", PassWord));

                var LoginDetails = await Task.Run(() => _dbContext.LoginDetail
                                .FromSqlRaw(@"exec CheckDealerLogin @UserName,@Password", param.ToArray()).ToListAsync());

                return LoginDetails;
           
            
        }
    }
}
