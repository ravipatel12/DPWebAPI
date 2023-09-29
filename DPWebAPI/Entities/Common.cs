using DPWebAPI.DBContexts;
using DPWebAPI.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DPWebAPI.Entities
{
    
    public class Common
    {
        [Key]
        public int Id { get; set; }
        public class UserDetails
        {
            [Key]
            public int UserId { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }


        }
        public class LoginDetails
        {
            [Key]
            public int UserId { get; set; }
            public string UserName { get; set; }
            public string EmployeeName { get; set; }
            public bool IsDealerLogin { get; set; }
            public int Employee { get; set; }
            public bool IsActive { get; set; }
            public DateTime PasswordExpiryDate { get; set; }


           

        }
        
    }
    public class Status
    {
        public int ResultId { get; set; }
        public string Result { get; set; }
    }
}
