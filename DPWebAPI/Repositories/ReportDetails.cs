using DPWebAPI.DBContexts;
using DPWebAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
namespace DPWebAPI.Repositories
{
    public class ReportDetails : IReportDetails
    {
        private readonly ApplicationDBContext _dbContext;

        public ReportDetails(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Common.WebOrderHistoryReport>> GetWebOrderHistoryReportAsync([FromBody]string xml)
        {


            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@XMLParam", xml));
            

            var WebOrderReport = await Task.Run(() => _dbContext.WebOrderReport
                            .FromSqlRaw(@"exec DPOrderHistory @XMLParam", param.ToArray()).ToListAsync());

            return WebOrderReport;


        }
    }
}
