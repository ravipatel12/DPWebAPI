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

namespace DPWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IUserDetails userService;
        private readonly ILoginDetails loginService;
        public ValuesController(ILoginDetails loginService)
        {
            this.loginService = loginService;
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

        [HttpGet("GetUserListAsync")]
        public async Task<List<Common.UserDetails>> GetUserListAsync()
        {
            try
            {
                return await userService.GetUserListAsync();

            }
            catch (Exception ex)
            {
                string a = ex.Message;
                throw;
            }
        }
    }
}
