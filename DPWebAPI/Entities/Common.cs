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
            public string LastLoginTime { get; set; }
            public bool Agent { get; set; }




        }
        public class WebOrderHistoryReport
        {
            
            public string WebOrderNo { get; set; }
            public string WebOrderDate { get; set; }
            public int SalesOrderID { get; set; }
            public string POExpiryDate { get; set; }
            public string SchDate { get; set; }
            public string RevSchDate { get; set; }
            public string ItemName { get; set; }
            public string ItemCode { get; set; }
            public decimal PackingSize { get; set; }
            public decimal OrderQty { get; set; }
            public decimal DispatchQty { get; set; }
            public  string? Status { get; set; }
            public string Party { get; set; }


        }
        public class ItemTypeMaster
        {
            public int ParentItemTypeID { get; set; }
            public string? ParentItemType { get; set; }
            public int ItemTypeID { get; set; }
            public string ItemType { get; set; }

        }
        public class PartyMaster
        {
            public int PartyID { get; set; }
            public string PartyCode { get; set; }
            public string Party { get; set; }
            

        }

    }
    public class Status
    {
        public int ResultId { get; set; }
        public string Result { get; set; }
    }
}
