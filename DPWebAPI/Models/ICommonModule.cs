using DPWebAPI.Entities;
namespace DPWebAPI.Models
{
    public interface ICommonModule
    {
        public Task<IEnumerable<Common.ItemTypeMaster>> GetItemTypeAsync(int ItemTypeId);
        public Task<IEnumerable<Common.ItemTypeMaster>> GetItemTypeWithChildAsync(int ItemTypeId);
        public Task<IEnumerable<Common.PartyMaster>> GetPartyAsync(int SalesPersonId,int AgentId,int DealerPartyId);
    }
}
