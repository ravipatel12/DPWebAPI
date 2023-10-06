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
        public ValuesController(ILoginDetails loginService,IReportDetails reportDetails, ICommonModule commonModule)
        {
            this.loginService = loginService;
            this.ReportService = reportDetails;
            CommonModule = commonModule;
        }

        [HttpGet("GetLoginDetails")]
        public async Task<IActionResult> GetLoginDetails(string UserName,string Password)
        {
           
            IEnumerable<Common.LoginDetails> result=null;
            var jsonData = new object();
            try
            {
                result = await loginService.GetLoginDetailsAsync(UserName,Password);
                jsonData = new { Data = result, StatusID = HttpStatusCode.OK, Status = "Success" };
            }
            catch(Exception ex)
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
        public async Task<IActionResult> GetParty(int SalesPersonId, int AgentId, int DealerPartyId)
        {

            IEnumerable<Common.PartyMaster> result = null;
            var jsonData = new object();
            try
            {
                result = await CommonModule.GetPartyAsync(SalesPersonId, AgentId, DealerPartyId);
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
