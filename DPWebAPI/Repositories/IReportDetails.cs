using DPWebAPI.Entities;
namespace DPWebAPI.Repositories
{
    public interface IReportDetails
    {
        public Task<IEnumerable<Common.WebOrderHistoryReport>> GetWebOrderHistoryReportAsync(string xml);
    }
}
