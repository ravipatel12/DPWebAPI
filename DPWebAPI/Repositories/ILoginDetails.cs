using DPWebAPI.Entities;

namespace DPWebAPI.Repositories
{
    public interface ILoginDetails
    {
        public Task<IEnumerable<Common.LoginDetails>> GetLoginDetailsAsync(string UserName, string PassWord);

        
    }
}
