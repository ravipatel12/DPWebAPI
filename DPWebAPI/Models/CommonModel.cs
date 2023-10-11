using DPWebAPI.DBContexts;
using DPWebAPI.Entities;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection.Metadata;
using System.Xml.Linq;

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
        public async Task<IEnumerable<Common.ItemMasterForPlaceOrder>> GetItemForPlaceOrderAsync(int PartyId, int Item1Type, int Item2Type, int Item3Type, int Item4Type, int SerchableItemId)
        {


            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@PartyId", PartyId));
            param.Add(new SqlParameter("@Item1Type", Item1Type));
            param.Add(new SqlParameter("@Item2Type", Item2Type));
            param.Add(new SqlParameter("@Item3Type", Item3Type));
            param.Add(new SqlParameter("@Item4Type", Item4Type));
            param.Add(new SqlParameter("@SerchableItemId", SerchableItemId));


            var ItemDetailsForPO = await Task.Run(() => _dbContext.ItemDetailsForPO
                            .FromSqlRaw(@"exec GetItemForJDP @PartyId,@Item1Type,@Item2Type,@Item3Type,@Item4Type,@SerchableItemId", param.ToArray()).ToListAsync());

            return ItemDetailsForPO;


        }
        public async Task<IEnumerable<Common.ItemMaster>> GetItemAsync()
        {

            var ItemDetails = await Task.Run(() => _dbContext.ItemDetails
                            .FromSqlRaw(@"exec GetItemForSearchJDP").ToListAsync());

            return ItemDetails;


        }
        public async Task<IEnumerable<Common.ErrorMessage>> SaveOrderAsync(string TableName,string xml)
        {
            List<Common.ErrorMessage> msg = new List<Common.ErrorMessage>();

           // IEnumerable<Common.ErrorMessage> enumerable = (IEnumerable<Common.ErrorMessage>)msg;


            var param = new SqlParameter[] {
            new SqlParameter() {
             ParameterName = "@TableName",
             SqlDbType =  System.Data.SqlDbType.VarChar,
             Direction = System.Data.ParameterDirection.Input,
             Value = TableName
            },
            new SqlParameter() {
             ParameterName = "@xml",
             SqlDbType =  System.Data.SqlDbType.VarChar,
             Direction = System.Data.ParameterDirection.Input,
             Value = xml
            },
            new SqlParameter() {
             ParameterName = "@ErroOut",
             SqlDbType =  System.Data.SqlDbType.Int,
             Direction = System.Data.ParameterDirection.Output,
            },
            new SqlParameter() {
             ParameterName = "@ErrMsg",
             SqlDbType =  System.Data.SqlDbType.VarChar,
             Direction = System.Data.ParameterDirection.Output,
             Size = 50
            }};




            //var ItemDetailsForPO = await Task.Run(() => _dbContext.ItemDetailsForPO
            //                .FromSqlRaw(@"exec SaveOrderDetailsForJDP @TableName,@xml,@ErroOut,@ErrMsg", param.ToArray()).ToListAsync());

            var sql = "exec SaveOrderDetailsForJDP @TableName,@xml,@ErroOut OUT,@ErrMsg OUT";
            var resultObj = _dbContext.Database.ExecuteSqlRaw(sql, param.ToArray());

            //msg.ErrorId = Convert.ToInt32(param[2].Value.ToString());
            //msg.ErrorMsg = param[3].Value.ToString();

            msg.Add(new Common.ErrorMessage { ErrorId = Convert.ToInt32(param[2].Value.ToString()), ErrorMsg = param[3].Value.ToString() });

            return msg;


        }
        public async Task<IEnumerable<Common.MailerData>> MailerDetailsAsync(int UserId, string ParameterValue)
        {


            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@ParameterValue", ParameterValue));
            param.Add(new SqlParameter("@UserId", UserId));
           


            var MailerDetails = await Task.Run(() => _dbContext.MailerDetails
                            .FromSqlRaw(@"exec sp_MailerFormDetails @ParameterValue,@UserId", param.ToArray()).ToListAsync());

            return MailerDetails;


        }
        public async Task<IEnumerable<Common.CartItem>> CardItemDetailsAsync(int UserId, int ClientId)
        {


            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@UserId", UserId));
            param.Add(new SqlParameter("@ClientId", ClientId));



            var CardItemDetails = await Task.Run(() => _dbContext.CardItemDetails
                            .FromSqlRaw(@"exec GetCartItemJDP @UserId,@ClientId", param.ToArray()).ToListAsync());

            return CardItemDetails;


        }
    }
    
}
