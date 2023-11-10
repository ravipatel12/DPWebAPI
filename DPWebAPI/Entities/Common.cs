﻿using DPWebAPI.DBContexts;
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
            public string Address { get; set; }
            public string State { get; set; }
            public string City { get; set; }
            public string Email { get; set; }
            public string SalesPersonEmailId { get; set; }


        }
        public class ItemMasterForPlaceOrder
        {
            [Key]
            public int ItemId { get; set; }
            public string ItemName { get; set; }
            public string ItemCode { get; set; }
            public string OurCode { get; set; }
            public string OurName { get; set; }
            public decimal PackingSize { get; set; }
            public decimal Rate { get; set; }
            public int BDL { get; set; }
            public int FactoryID { get; set; }
            public int ItemTypeId { get; set; }
            public string? Series { get; set; }
            public string? UOM { get; set; }
            public int ProjectId { get; set; }




        }
        public class ItemMaster
        {
            [Key]
            public int ItemId { get; set; }
            public string ItemName { get; set; }
            public string ItemCode { get; set; }
            public string OurCode { get; set; }
            public string OurName { get; set; }
           
        }
        public class MailerData
        {
            
            public string? MailFrom { get; set; }
            public string?NewTmp { get; set; }
            public string?MailCC { get; set; }
            public string?MailBCC { get; set; }
            public string?MailServer { get; set; }
            public string?MailUserName { get; set; }
            public int? MailPort { get; set; }
            public string? CommToMailId { get; set; }
            public bool? SSL { get; set; }
            public string? ContactUsMailId { get; set; }

        }
        public class CartItem
        {
            [Key]
            public int WebOrderID { get; set; }
            public string? OurName { get; set; }
            public string? OurCode { get; set; }
            public string? ItemCode { get; set; }
            public string? ItemName { get; set; }
            public decimal PackingSize { get; set; }
            public decimal NoOfBoxes { get; set; }
            public int FactoryId { get; set; }
            public string? ShortName { get; set; }
            public string? FactoryIds { get; set; }
            public decimal Rate { get; set; }
            public string? Series { get; set; }
            public string? UOM { get; set; }
            public decimal Qty { get; set; }
            public decimal OrderValue { get; set;}

        }
        public class ErrorMessage
        {
            public int ErrorId { get; set; }
            public string ErrorMsg { get; set; }
        }

        public class WebOrderConfirm
        {
            [Key]
            public int SRNo { get; set; }
            public string? OrderNo { get; set; }
            public string? OrderDate { get; set; }
            public string? ItemName { get; set; }
            public string? ItemCode { get; set; }
            public decimal NoOfBoxes { get; set; }
            public string? Party { get; set; }
            public string? Address { get; set; }
            public string? Remark { get; set; }
            public string? OrderRemark { get; set; }
            public decimal Rate { get; set; }
            public int Qty { get; set; }
            public decimal Value { get; set; }

        }

        public class WebOrderToSo
        {
            [Key]
            public int WebOrderID { get; set; }
            public string? WebOrderNo { get; set; }
            public string? WebOrderDate { get; set; }
            public int ClientId { get; set; }
            public string? Party { get; set; }
            public int YearId { get; set; }
            public string? UserName { get; set; }
            public int ItemID { get; set; }
            public decimal PackingSize { get; set; }
            public decimal NoOfBoxes { get; set; }
            public string? ItemCode { get; set; }
            public string? ItemName { get; set; }
            public decimal Rate { get; set; }
            public string? Remarks { get; set; }
            public int FactoryId { get; set; }
            public string? OrderRemark { get; set; }
            public string? ProjectName { get; set; }
            public decimal Qty { get; set;}
           


            
            

        }

        /// <summary>
        /// public class
        /// </summary>

        public class Factory
        {
            public int FactoryID { get; set; }
            public string? FactoryName { get; set;}
            
        }

        public class DispatchSummary
        {
            [Key]
            public string? InvoiceNo { get; set; }
            public DateTime InvoiceDate { get; set; }
            public string? ClientCode { get; set; }
            public string? ClientName { get; set; }
            public string? ClientGST { get; set; }
            public string? City { get; set; }
            public string? State { get; set; }
            public int Boxes { get; set; }
            public decimal DispatchQuantity { get; set; }
            public decimal TaxableAmount { get; set; }
            public decimal GSTAmount { get; set; }
            public decimal TCS { get; set; }
            public decimal InvoiceAmount { get; set; }
            public string? lrno { get; set; }
            public string? lrdate { get; set; }
            public string? Transporter { get; set; }
            public string? vehicleno { get; set; }
            public string? EwayBillNo { get; set; }
            public string? EwayBillDate { get; set; }
            public string? OrderDetails { get; set; }

            
        }
    }
    
}
