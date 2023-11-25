using DPWebAPI.DBContexts;
using DPWebAPI.Entities;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NuGet.Configuration;
using System.Data.SqlTypes;
using System.Data;
using System.Reflection.Metadata;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Threading;
using Newtonsoft.Json;
using static DPWebAPI.Entities.Common;

namespace DPWebAPI.Models
{
    public class CommonModel : ICommonModule
    {
        private readonly ApplicationDBContext _dbContext;
        private SqlConnection sqlConn;
        public CommonModel(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
           
        }
        #region start DB Connection any body don't try to make a changes on this block 
        public void OpenConnection()
        {

            using (var cnn = _dbContext.Database.GetDbConnection())
            {

                try
                {
                    sqlConn = new SqlConnection(cnn.ConnectionString.ToString());
                    sqlConn.Open();

                   
                }
                catch (Exception ex)
                {
                    //throw ex;
                    if (sqlConn.State != ConnectionState.Closed)
                    {
                        sqlConn.Close();

                    }
                }
            }
        }
        public IDbCommand CreateCommand(string procName, IDbDataParameter[] @params)
        {
            OpenConnection();
            SqlCommand sqlCmd = null;
            sqlCmd = new SqlCommand(procName, sqlConn);
            sqlCmd.CommandTimeout = 600;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            if (@params != null)
            {
                foreach (SqlParameter parameter in @params)
                {
                    sqlCmd.Parameters.Add(parameter);
                }
            }
            sqlCmd.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return (IDbCommand)sqlCmd;
        }
        public int ExecuteProc(string procName)
        {
            SqlCommand sqlCmd = (SqlCommand)CreateCommand(procName, null);
            sqlCmd.ExecuteNonQuery();
            sqlCmd.Dispose();
            this.Dispose();
            return (int)sqlCmd.Parameters["ReturnValue"].Value;
        }

        public int ExecuteProc(string procName, IDbDataParameter[] @params)
        {
            SqlCommand sqlCmd = (SqlCommand)CreateCommand(procName, @params);
            sqlCmd.ExecuteNonQuery();
            sqlCmd.Dispose();
            this.Dispose();
            return (int)sqlCmd.Parameters["ReturnValue"].Value;
        }

        public void ExecuteProc(string procName, ref DataSet dataSetObject)
        {
            SqlCommand sqlCmd = (SqlCommand)CreateCommand(procName, null);
            SqlDataAdapter dtAdapter = new SqlDataAdapter(sqlCmd);
            DataSet dtSetObject = new DataSet();
            dtAdapter.Fill(dtSetObject);
            dataSetObject = dtSetObject;
            sqlCmd.Dispose();
            this.Dispose();
        }

        public void ExecuteProc(string procName, IDbDataParameter[] @params, ref DataSet dataSetObject)
        {
            SqlCommand sqlCmd = (SqlCommand)CreateCommand(procName, @params);
            SqlDataAdapter dtAdapter = new SqlDataAdapter(sqlCmd);
            DataSet dtSetObject = new DataSet();
            dtAdapter.Fill(dtSetObject);
            dataSetObject = dtSetObject;

            sqlCmd.Dispose();
            this.Dispose();
        }

        public void ExecuteProc(string procName, IDbDataParameter[] @params, ref DataTable dataSetObject)
        {
            SqlCommand sqlCmd = (SqlCommand)CreateCommand(procName, @params);
            SqlDataAdapter dtAdapter = new SqlDataAdapter(sqlCmd);
            dtAdapter.Fill(dataSetObject);
            sqlCmd.Dispose();
            this.Dispose();
        }

        public void ExecuteProc(string procName, IDbDataParameter[] @params, ref IDataReader dataReader)
        {
            SqlCommand sqlCmd = (SqlCommand)CreateCommand(procName, @params);
            dataReader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public void ExecuteProc(string procName, ref DataTable dataSetObject)
        {
            SqlCommand sqlCmd = (SqlCommand)CreateCommand(procName, null);
            SqlDataAdapter dtAdapter = new SqlDataAdapter(sqlCmd);
            DataTable dtSetObject = new DataTable();
            dtAdapter.Fill(dtSetObject);
            dataSetObject = dtSetObject;
            sqlCmd.Dispose();
            this.Dispose();
        }
        public void Dispose()
        {
            if (!(System.Convert.IsDBNull(sqlConn)))
            {
                sqlConn.Dispose();
                sqlConn = null;
            }
        }

        public void Close()
        {
            if (!(System.Convert.IsDBNull(sqlConn)))
            {
                sqlConn.Close();
            }
        }
        #endregion

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
        public async Task<IEnumerable<Common.ErrorMessage>> DeleteItemAsync(int WebOrderId)
        {
            List<Common.ErrorMessage> msg = new List<Common.ErrorMessage>();

            // IEnumerable<Common.ErrorMessage> enumerable = (IEnumerable<Common.ErrorMessage>)msg;


            var param = new SqlParameter[] {
            new SqlParameter() {
             ParameterName = "@WebOrderId",
             SqlDbType =  System.Data.SqlDbType.Int,
             Direction = System.Data.ParameterDirection.Input,
             Value = WebOrderId
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

            var sql = "exec DeleteCartItemJDP @WebOrderId,@ErroOut OUT,@ErrMsg OUT";
            var resultObj = _dbContext.Database.ExecuteSqlRaw(sql, param.ToArray());

            //msg.ErrorId = Convert.ToInt32(param[2].Value.ToString());
            //msg.ErrorMsg = param[3].Value.ToString();

            msg.Add(new Common.ErrorMessage { ErrorId = Convert.ToInt32(param[1].Value.ToString()), ErrorMsg = param[2].Value.ToString() });

            return msg;


        }
        public async Task<IEnumerable<Common.WebOrderConfirm>> ConfirmOrderAsync(string OrderRemark, string TableName, string xml)
        {
            


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
             ParameterName = "@OrderRemark",
             SqlDbType =  System.Data.SqlDbType.VarChar,
             Direction = System.Data.ParameterDirection.Input,
             Value = (object)OrderRemark ?? DBNull.Value
        }
    };

            var webOrderDetails = await Task.Run(() => _dbContext.webOrderDetails
                            .FromSqlRaw(@"exec SPConfirmOrderJDP @TableName,@xml,@OrderRemark", param.ToArray()).ToListAsync());

            return webOrderDetails;


        }

        public async Task<string> GetPendingwebOrderForSOAsync()
        {
            
            DataSet ds=new DataSet();
            ExecuteProc("GetPendingwebOrderForSO", ref ds);
            string data = JsonConvert.SerializeObject(ds, Formatting.Indented);
            //var wotosoDetails = JsonConvert.DeserializeObject<IEnumerable<IActionResult>>(data);
            return data;
        }
        public async Task<IEnumerable<Common.ErrorMessage>> ConvertWOTOSOAsync(string TableName, string xml)
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



            var sql = "exec SPConvertWOTOSOForJDP @TableName,@xml,@ErroOut OUT,@ErrMsg OUT";
            var resultObj = _dbContext.Database.ExecuteSqlRaw(sql, param.ToArray());

           

            msg.Add(new Common.ErrorMessage { ErrorId = Convert.ToInt32(param[2].Value.ToString()), ErrorMsg = param[3].Value.ToString() });

            return msg;


        }

        public async Task<IEnumerable<Common.DispatchSummary>> GetDispatchSummaryAsync(string FromDate, string Todate, string PartyIDs, int ReportType, int UserID, int PartyID)
        {


            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@FromDate", FromDate));
            param.Add(new SqlParameter("@ToDate", Todate));
            param.Add(new SqlParameter("@PartyIDs", PartyIDs));
            param.Add(new SqlParameter("@ReportType", ReportType));
            param.Add(new SqlParameter("@UserID", UserID));
            param.Add(new SqlParameter("@PartyID", PartyID));


            var Dispatch = await Task.Run(() => _dbContext.Dispatchs
                            .FromSqlRaw(@"exec DPDispatchDetails @FromDate,@ToDate,@PartyIDs,@ReportType, @UserID,@PartyID", param.ToArray()).ToListAsync());

            return Dispatch;


        }

        public async Task<IEnumerable<Common.AccountsLedgerDetails>> GeAccountsLedgerDetailsAsync(int CompanyID, string FactoryId, DateTime FromDate, DateTime Todate, int PartyID)
        {


            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@CompanyID", CompanyID));
            param.Add(new SqlParameter("@FactoryID", FactoryId));
            param.Add(new SqlParameter("@FromDate", FromDate));
            param.Add(new SqlParameter("@ToDate", Todate));
            param.Add(new SqlParameter("@PartyID", PartyID));
           


            var LegderDetails = await Task.Run(() => _dbContext.LegderDetails
                            .FromSqlRaw(@"exec sp_GetMultipleLedgerDP @CompanyID,@FactoryID,@FromDate,@ToDate,@PartyID", param.ToArray())
                            .ToListAsync());

            return LegderDetails;


        }

        public async Task<IEnumerable<Common.DispatchDetails>> GetDispatchDetailAsync(string FromDate, string Todate, string PartyIDs, int ReportType, int UserID, int PartyID)
        {


            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@FromDate", FromDate));
            param.Add(new SqlParameter("@ToDate", Todate));
            param.Add(new SqlParameter("@PartyIDs", PartyIDs));
            param.Add(new SqlParameter("@ReportType", ReportType));
            param.Add(new SqlParameter("@UserID", UserID));
            param.Add(new SqlParameter("@PartyID", PartyID));


            var Dispatch = await Task.Run(() => _dbContext.DispatachD
                            .FromSqlRaw(@"exec DPDispatchDetails @FromDate,@ToDate,@PartyIDs,@ReportType, @UserID,@PartyID", param.ToArray()).ToListAsync());

            return Dispatch;


        }

        public async Task<IEnumerable<Common.ComapanyMaster>> GetComapanyMasterAsync(int UserID)
        {
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@pUserID", UserID));

            var Company = await Task.Run(() => _dbContext.Company
                            .FromSqlRaw(@"exec GetDPCompany @pUserID", param.ToArray()).ToListAsync());

            return Company;
        }
    }

}
