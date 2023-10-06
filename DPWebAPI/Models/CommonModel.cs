using DPWebAPI.DBContexts;
using DPWebAPI.Entities;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
namespace DPWebAPI.Models
{
    public class CommonModel : ICommonModule
    {
        private readonly ApplicationDBContext _dbContext;

        public CommonModel(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Common.ItemTypeMaster>> GetItemTypeAsync(int ItemTypeId)
        {


            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@ItemTypeId", ItemTypeId));
            

            var ItemType = await Task.Run(() => _dbContext.ItemType
                            .FromSqlRaw(@"exec GetItemTypeForDPJ @ItemTypeId", param.ToArray()).ToListAsync());

            return ItemType;


        }
        public async Task<IEnumerable<Common.ItemTypeMaster>> GetItemTypeWithChildAsync(int ItemTypeId)
        {


            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@ItemTypeId", ItemTypeId));


            var ItemType = await Task.Run(() => _dbContext.ItemType
                            .FromSqlRaw(@"exec GetItemTypeWithChildDPJ @ItemTypeId", param.ToArray()).ToListAsync());

            return ItemType;


        }
        public async Task<IEnumerable<Common.PartyMaster>> GetPartyAsync(int SalesPersonId, int AgentId, int DealerPartyId)
        {


            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@SalesPersonID", SalesPersonId));
            param.Add(new SqlParameter("@AgentID", AgentId));
            param.Add(new SqlParameter("@DealerPartyID", DealerPartyId));


            var PartyMaster = await Task.Run(() => _dbContext.PartyDetails
                            .FromSqlRaw(@"exec GetDPPartyList @SalesPersonID,@AgentID,@DealerPartyID", param.ToArray()).ToListAsync());

            return PartyMaster;


        }


    }
    
}
