using DPWebAPI.Entities;

namespace DPWebAPI.Repositories
{
    public interface IUserDetails
    {
        public Task<List<Common.UserDetails>> GetUserListAsync();
        public Task<IEnumerable<Common.UserDetails>> GetUserByIdAsync(int Id);
        public Task<int> AddUserAsync(Common.UserDetails user);
    }
}
