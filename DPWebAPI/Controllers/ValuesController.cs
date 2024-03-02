using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DPWebAPI.DBContexts;
using DPWebAPI.Entities;
using DPWebAPI.Repositories;
using System.Collections;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using DPWebAPI.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Xml.Linq;
using Microsoft.AspNetCore.Cors;

namespace DPWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IUserDetails userService;
        private readonly ILoginDetails loginService;
        private readonly IReportDetails ReportService;
        private readonly ICommonModule CommonModule;
        public ValuesController(ILoginDetails loginService, IReportDetails reportDetails, ICommonModule commonModule)
        {
            this.loginService = loginService;
            this.ReportService = reportDetails;
            CommonModule = commonModule;
        }

        [HttpGet("GetLoginDetails")]
        public async Task<IActionResult> GetLoginDetails(string UserName, string Password)
        {

            IEnumerable<Common.LoginDetails> result = null;
            var jsonData = new object();
            try
            {
                result = await loginService.GetLoginDetailsAsync(UserName, Password);
                jsonData = new { Data = result, StatusID = HttpStatusCode.OK, Status = "Success" };
            }
            catch (Exception ex)
            {

                jsonData = new { Data = result, StatusID = HttpStatusCode.BadRequest, Status = ex.Message };

            }

            return new JsonResult(jsonData);
        }
        [HttpGet("GetWebOrderHistoryReport")]
        //[Route("AjaxMethod")]
        //[Consumes("application/xml", "application/json")]
        //[Produces("application/xml", "application/json")]
        //[EnableCors("AllowAllHeaders")]
        //public async Task<IActionResult> GetWebOrderHistoryReport([FromBody] XElement xml)
        public async Task<IActionResult> GetWebOrderHistoryReport(string xml)
        {

            IEnumerable<Common.WebOrderHistoryReport> result = null;
            var jsonData = new object();
            try
            {
                result = await ReportService.GetWebOrderHistoryReportAsync(xml.ToString());
                jsonData = new { Data = result, StatusID = HttpStatusCode.OK, Status = "Success" };
            }
            catch (Exception ex)
            {

                jsonData = new { Data = result, StatusID = HttpStatusCode.BadRequest, Status = ex.Message };

            }

            return new JsonResult(jsonData);
        }
        [HttpGet("GetItemType")]
        public async Task<IActionResult> GetItemType(int ItemTypeId)
        {

            IEnumerable<Common.ItemTypeMaster> result = null;
            var jsonData = new object();
            try
            {
                result = await CommonModule.GetItemTypeAsync(ItemTypeId);
                jsonData = new { Data = result, StatusID = HttpStatusCode.OK, Status = "Success" };
            }
            catch (Exception ex)
            {

                jsonData = new { Data = result, StatusID = HttpStatusCode.BadRequest, Status = ex.Message };

            }

            return new JsonResult(jsonData);
        }

        [HttpGet("GetItemTypeWithChild")]
        public async Task<IActionResult> GetItemTypeWithChild(int ItemTypeId)
        {

            IEnumerable<Common.ItemTypeMaster> result = null;
            var jsonData = new object();
            try
            {
                result = await CommonModule.GetItemTypeWithChildAsync(ItemTypeId);
                jsonData = new { Data = result, StatusID = HttpStatusCode.OK, Status = "Success" };
            }
            catch (Exception ex)
            {

                jsonData = new { Data = result, StatusID = HttpStatusCode.BadRequest, Status = ex.Message };

            }

            return new JsonResult(jsonData);
        }
        [HttpGet("GetParty")]
        public async Task<IActionResult> GetParty(int SalesPersonId, int AgentId, int DealerPartyId,bool IsAdmin)
        {

            IEnumerable<Common.PartyMaster> result = null;
            var jsonData = new object();
            try
            {
                result = await CommonModule.GetPartyAsync(SalesPersonId, AgentId, DealerPartyId, IsAdmin);
                jsonData = new { Data = result, StatusID = HttpStatusCode.OK, Status = "Success" };
            }
            catch (Exception ex)
            {

                jsonData = new { Data = result, StatusID = HttpStatusCode.BadRequest, Status = ex.Message };

            }

            return new JsonResult(jsonData);
        }
        [HttpGet("GetItemForPlaceOrder")]
        public async Task<IActionResult> GetItemForPlaceOrder(int PartyId, int Item1Type, int Item2Type, int Item3Type, int Item4Type, int SerchableItemId)
        {

            IEnumerable<Common.ItemMasterForPlaceOrder> result = null;
            var jsonData = new object();
            try
            {
                result = await CommonModule.GetItemForPlaceOrderAsync(PartyId, Item1Type, Item2Type, Item3Type, Item4Type, SerchableItemId);
                jsonData = new { Data = result, StatusID = HttpStatusCode.OK, Status = "Success" };
            }
            catch (Exception ex)
            {

                jsonData = new { Data = result, StatusID = HttpStatusCode.BadRequest, Status = ex.Message };

            }

            return new JsonResult(jsonData);
        }


        [HttpGet("GetItem")]
        public async Task<IActionResult> GetItem()
        {

            IEnumerable<Common.ItemMaster> result = null;
            var jsonData = new object();
            try
            {
                result = await CommonModule.GetItemAsync();
                jsonData = new { Data = result, StatusID = HttpStatusCode.OK, Status = "Success" };
            }
            catch (Exception ex)
            {

                jsonData = new { Data = result, StatusID = HttpStatusCode.BadRequest, Status = ex.Message };

            }

            return new JsonResult(jsonData);
        }
        [HttpPost("SaveOrder")]
        //[Route("AjaxMethod")]
        [Consumes("application/xml", "application/json")]
        [Produces("application/xml", "application/json")]
        //[EnableCors("AllowAllHeaders")]
        //public async Task<IActionResult> GetWebOrderHistoryReport([FromBody] XElement xml)
        public async Task<IActionResult> SaveOrder(string TableName, [FromBody] XElement xml)
        {

            IEnumerable<Common.ErrorMessage> result = null;
            var jsonData = new object();
            try
            {
                result = await CommonModule.SaveOrderAsync(TableName, xml.ToString());
                jsonData = new { Data = result, StatusID = HttpStatusCode.OK, Status = "Success" };
            }
            catch (Exception ex)
            {


                jsonData = new { Data = result, StatusID = HttpStatusCode.BadRequest, Status = ex.Message };

            }

            return new JsonResult(jsonData);
        }
        [HttpGet("MailerDetails")]
        public async Task<IActionResult> MailerDetails(int UserId, string ParameterValue)
        {

            IEnumerable<Common.MailerData> result = null;
            var jsonData = new object();
            try
            {
                result = await CommonModule.MailerDetailsAsync(UserId, ParameterValue);
                jsonData = new { Data = result, StatusID = HttpStatusCode.OK, Status = "Success" };
            }
            catch (Exception ex)
            {

                jsonData = new { Data = result, StatusID = HttpStatusCode.BadRequest, Status = ex.Message };

            }

            return new JsonResult(jsonData);
        }
        [HttpGet("CardItemDetails")]
        public async Task<IActionResult> CardItemDetails(int UserId, int ClientId)
        {

            IEnumerable<Common.CartItem> result = null;
            var jsonData = new object();
            try
            {
                result = await CommonModule.CardItemDetailsAsync(UserId, ClientId);
                jsonData = new { Data = result, StatusID = HttpStatusCode.OK, Status = "Success" };
            }
            catch (Exception ex)
            {

                jsonData = new { Data = result, StatusID = HttpStatusCode.BadRequest, Status = ex.Message };

            }

            return new JsonResult(jsonData);
        }
        [HttpPost("DeleteItem")]
        //[Route("AjaxMethod")]
        [Consumes("application/xml", "application/json")]
        [Produces("application/xml", "application/json")]
        //[EnableCors("AllowAllHeaders")]
        //public async Task<IActionResult> GetWebOrderHistoryReport([FromBody] XElement xml)
        public async Task<IActionResult> DeleteItem(int WebOrderId)
        {

            IEnumerable<Common.ErrorMessage> result = null;
            var jsonData = new object();
            try
            {
                result = await CommonModule.DeleteItemAsync(WebOrderId);
                jsonData = new { Data = result, StatusID = HttpStatusCode.OK, Status = "Success" };
            }
            catch (Exception ex)
            {


                jsonData = new { Data = result, StatusID = HttpStatusCode.BadRequest, Status = ex.Message }; 

            }

            return new JsonResult(jsonData); 
        }
        [HttpPost("ConfirmOrder")]
        //[Route("AjaxMethod")]
        [Consumes("application/xml", "application/json")]
        [Produces("application/xml", "application/json")]
        //[EnableCors("AllowAllHeaders")]
        //public async Task<IActionResult> GetWebOrderHistoryReport([FromBody] XElement xml)
        public async Task<IActionResult> ConfirmOrder(string? OrderRemark, string TableName, [FromBody] XElement xml)
        {

            IEnumerable<Common.WebOrderConfirm> result = null;
            var jsonData = new object();
            try
            {
                result = await CommonModule.ConfirmOrderAsync(OrderRemark, TableName, xml.ToString());
                jsonData = new { Data = result, StatusID = HttpStatusCode.OK, Status = "Success" };
            }
            catch (Exception ex)
            {


                jsonData = new { Data = result, StatusID = HttpStatusCode.BadRequest, Status = ex.Message };

            }

            return new JsonResult(jsonData);
        }
        [HttpGet("GetPendingwebOrderForSO")]
        public async Task<IActionResult> GetPendingwebOrderForSO()
        {

            string result = null;
            var jsonData = new object();
            try
            {
                result = await CommonModule.GetPendingwebOrderForSOAsync();
                jsonData = new { Data = result, StatusID = HttpStatusCode.OK, Status = "Success" };
            }
            catch (Exception ex)
            {

                jsonData = new { Data = result, StatusID = HttpStatusCode.BadRequest, Status = ex.Message };

            }

            return new JsonResult(jsonData);
        }
        [HttpPost("ConvertWOTOSO")]
        //[Route("AjaxMethod")]
        [Consumes("application/xml", "application/json")]
        [Produces("application/xml", "application/json")]
        //[EnableCors("AllowAllHeaders")]
        //public async Task<IActionResult> GetWebOrderHistoryReport([FromBody] XElement xml)ConfirmOrder
        public async Task<IActionResult> ConvertWOTOSO(string TableName, [FromBody] XElement xml)
        {

            IEnumerable<Common.ErrorMessage> result = null;
            var jsonData = new object();
            try
            {
                result = await CommonModule.ConvertWOTOSOAsync(TableName, xml.ToString());
                jsonData = new { Data = result, StatusID = HttpStatusCode.OK, Status = "Success" };
            }
            catch (Exception ex)
            {


                jsonData = new { Data = result, StatusID = HttpStatusCode.BadRequest, Status = ex.Message };

            }

            return new JsonResult(jsonData);
        }
        [HttpGet("GetDispatchSummary")]
        public async Task<IActionResult> GetDispatchSummary(string FromDate, string Todate, string PartyIDs, int ReportType, int UserID, int PartyID)
        {

            IEnumerable<Common.DispatchSummary> result = null;
            var jsonData = new object();
            try
            {
                result = await CommonModule.GetDispatchSummaryAsync(FromDate, Todate, PartyIDs, ReportType, UserID, PartyID);
                jsonData = new { Data = result, StatusID = HttpStatusCode.OK, Status = "Success" };
            }
            catch (Exception ex)
            {

                jsonData = new { Data = result, StatusID = HttpStatusCode.BadRequest, Status = ex.Message };

            }

            return new JsonResult(jsonData);

        }

        [HttpGet("GetAccountsLedgerDetails")]
        public async Task<IActionResult> GetAccountsLedgerDetails(int CompanyID, string FactoryId, DateTime FromDate, DateTime Todate, int PartyID)
        {

            IEnumerable<Common.AccountsLedgerDetails> result = null;
            var jsonData = new object();
            try
            {
                result = await CommonModule.GeAccountsLedgerDetailsAsync(CompanyID,FactoryId,FromDate,Todate,PartyID);
                jsonData = new { Data = result, StatusID = HttpStatusCode.OK, Status = "Success" };
            }
            catch (Exception ex)
            {

                jsonData = new { Data = result, StatusID = HttpStatusCode.BadRequest, Status = ex.Message };

            }

            return new JsonResult(jsonData);

        }

        [HttpGet("GetDispatchDetail")]
        public async Task<IActionResult> GetDispatchDetail(string FromDate, string Todate, string PartyIDs, int ReportType, int UserID, int PartyID)
        {

            IEnumerable<Common.DispatchDetails> result = null;
            var jsonData = new object();
            try
            {
                result = await CommonModule.GetDispatchDetailAsync(FromDate, Todate, PartyIDs, ReportType, UserID, PartyID);
                jsonData = new { Data = result, StatusID = HttpStatusCode.OK, Status = "Success" };
            }
            catch (Exception ex)
            {

                jsonData = new { Data = result, StatusID = HttpStatusCode.BadRequest, Status = ex.Message };
            }

            return new JsonResult(jsonData);

        }

        [HttpGet("GetCompany")]
        public async Task<IActionResult> GetCompany(int UserID)
        {

            IEnumerable<Common.ComapanyMaster> result = null;
            var jsonData = new object();
            try
            {
                result = await CommonModule.GetComapanyMasterAsync(UserID);
                jsonData = new { Data = result, StatusID = HttpStatusCode.OK, Status = "Success" };
            }
            catch (Exception ex)
            {
                jsonData = new { Data = result, StatusID = HttpStatusCode.BadRequest, Status = ex.Message };
            }

            return new JsonResult(jsonData);
        }

        [HttpGet("GetAccountsOutstandingSummary")]
        public async Task<IActionResult> GetAccountsOutstandingSummary(int CompanyID, int ReportType, DateTime ToDate, int PartyId)
        {

            IEnumerable<Common.AccountsOutstandingSummary> result = null;
            var jsonData = new object();
            try
            {
                result = await CommonModule.GetAccountsOutstandingSummariesAsync(CompanyID, ReportType, ToDate, PartyId);
                jsonData = new { Data = result, StatusID = HttpStatusCode.OK, Status = "Success" };
            }
            catch (Exception ex)
            {

                jsonData = new { Data = result, StatusID = HttpStatusCode.BadRequest, Status = ex.Message };

            }

            return new JsonResult(jsonData);
        }

        [HttpGet("GetAccountsOutstandingDetails")]
        public async Task<IActionResult> GetAccountsOutstandingDetails(int CompanyID, int ReportType, DateTime ToDate, int PartyId)
        {

            IEnumerable<Common.AccountsOutstandingDetails> result = null;
            var jsonData = new object();
            try
            {
                result = await CommonModule.GetAccountsOutstandingDetailsAsync(CompanyID, ReportType, ToDate, PartyId);
                jsonData = new { Data = result, StatusID = HttpStatusCode.OK, Status = "Success" };
            }
            catch (Exception ex)
            {

                jsonData = new { Data = result, StatusID = HttpStatusCode.BadRequest, Status = ex.Message };

            }

            return new JsonResult(jsonData);
        }

        [HttpGet("GetBannerCatalog")]
        public async Task<IActionResult> GetBannerCatalog()
        {

            string result = null;
            var jsonData = new object();
            try
            {
                result = await CommonModule.GetBannerCatalogAsync();
                jsonData = new { Data = result, StatusID = HttpStatusCode.OK, Status = "Success" };
            }
            catch (Exception ex)
            {

                jsonData = new { Data = result, StatusID = HttpStatusCode.BadRequest, Status = ex.Message };

            }

            return new JsonResult(jsonData);
        }


        [HttpGet("GetDPPageType")]
        public async Task<IActionResult> GetDPPageType()
        {

            IEnumerable<Common.DPPageMaster> result = null;
            var jsonData = new object();
            try
            {
                result = await CommonModule.GetDPPageTypeAsync();
                jsonData = new { Data = result, StatusID = HttpStatusCode.OK, Status = "Success" };
            }
            catch (Exception ex)
            {

                jsonData = new { Data = result, StatusID = HttpStatusCode.BadRequest, Status = ex.Message };

            }

            return new JsonResult(jsonData);
        }


        [HttpPost("SaveDPMasterData")]
        //[Route("AjaxMethod")]
        [Consumes("application/xml", "application/json")]
        [Produces("application/xml", "application/json")]
        //[EnableCors("AllowAllHeaders")]
        //public async Task<IActionResult> GetWebOrderHistoryReport([FromBody] XElement xml)
        public async Task<IActionResult> SaveDPMasterData(string TableName, string TableName1,int UserID, [FromBody] XElement xml)
        {

            IEnumerable<Common.ErrorMessage> result = null;
            var jsonData = new object();
            try
            {
                result = await CommonModule.SaveDPMasterDataAsync(TableName, TableName1, UserID, xml.ToString());
                jsonData = new { Data = result, StatusID = HttpStatusCode.OK, Status = "Success" };
            }
            catch (Exception ex)
            {


                jsonData = new { Data = result, StatusID = HttpStatusCode.BadRequest, Status = ex.Message };

            }

            return new JsonResult(jsonData);
        }


        [HttpGet("GetBannerCatelogueForView")]
        public async Task<IActionResult> GetBannerCatelogueForView()
        {

            string result = null;
            var jsonData = new object();
            try
            {
                result = await CommonModule.GetBannerCatelogueForViewAsync();
                jsonData = new { Data = result, StatusID = HttpStatusCode.OK, Status = "Success" };
            }
            catch (Exception ex)
            {

                jsonData = new { Data = result, StatusID = HttpStatusCode.BadRequest, Status = ex.Message };

            }

            return new JsonResult(jsonData);
        }

        [HttpPost("SaveEnquiry")]
        //[Route("AjaxMethod")]
        [Consumes("application/xml", "application/json")]
        [Produces("application/xml", "application/json")]
        //[EnableCors("AllowAllHeaders")]
        //public async Task<IActionResult> GetWebOrderHistoryReport([FromBody] XElement xml)
        public async Task<IActionResult> SaveEnquiry(string TableName,int UserID, [FromBody] XElement xml)
        {

            //IEnumerable<Common.ErrorMessage> result = null;
            string result = "";
            var jsonData = new object();
            int message = 0;
            try
            {
                result = await CommonModule.SaveEnquiry(TableName, UserID, xml.ToString());
                jsonData = new { Data = result, StatusID = HttpStatusCode.OK, Status = "Success" };
              


            }
            catch (Exception ex)
            {


                jsonData = new { Data = result, StatusID = HttpStatusCode.BadRequest, Status = ex.Message };

            }


            return new JsonResult(jsonData);
        }

    }
}
