using DPWebAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace DPWebAPI.Models
{
    public interface ICommonModule
    {
        public Task<IEnumerable<Common.ItemTypeMaster>> GetItemTypeAsync(int ItemTypeId);
        public Task<IEnumerable<Common.ItemTypeMaster>> GetItemTypeWithChildAsync(int ItemTypeId);
        public Task<IEnumerable<Common.PartyMaster>> GetPartyAsync(int SalesPersonId,int AgentId,int DealerPartyId);
        public Task<IEnumerable<Common.ItemMasterForPlaceOrder>> GetItemForPlaceOrderAsync(int PartyId, int Item1Type, int Item2Type, int Item3Type, int Item4Type,int SerchableItemId);
        public Task<IEnumerable<Common.ItemMaster>> GetItemAsync();
        public Task<IEnumerable<Common.ErrorMessage>> SaveOrderAsync(string TableName, string xml);
        public Task<IEnumerable<Common.MailerData>> MailerDetailsAsync(int UserId, string ParameterValue);
        public Task<IEnumerable<Common.CartItem>> CardItemDetailsAsync(int UserId, int ClientId);
        public Task<IEnumerable<Common.ErrorMessage>> DeleteItemAsync(int WebOrderId);
        public Task<IEnumerable<Common.WebOrderConfirm>> ConfirmOrderAsync(string OrderRemark,string TableName, string xml);
        public Task<string> GetPendingwebOrderForSOAsync();
        public Task<IEnumerable<Common.ErrorMessage>> ConvertWOTOSOAsync(string TableName, string xml);
        
        public Task<IEnumerable<Common.AccountsLedgerDetails>> GeAccountsLedgerDetailsAsync(int CompanyID,string FactoryId, DateTime FromDate, DateTime Todate, int PartyID);
        public Task<IEnumerable<Common.DispatchSummary>> GetDispatchSummaryAsync(string FromDate, string ToDate, string PartyIDs, int ReportType, int UserID, int PartyID);

        public Task<IEnumerable<Common.DispatchDetails>> GetDispatchDetailAsync(string FromDate, string ToDate, string PartyIDs, int ReportType, int UserID, int PartyID);

        public Task<IEnumerable<Common.ComapanyMaster>> GetComapanyMasterAsync(int UserId);

        public Task<IEnumerable<Common.AccountsOutstandingDetails>> GetAccountsOutstandingDetailsAsync(int UserId);
    }
}
