using Microsoft.EntityFrameworkCore;

using DPWebAPI.Entities;
using DPWebAPI.Models;

namespace DPWebAPI.DBContexts
{
    public class ApplicationDBContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public ApplicationDBContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Common.WebOrderHistoryReport>()
                .HasNoKey()
                .ToView("WebOrderHistoryView");
            modelBuilder.Entity<Common.ItemTypeMaster>()
                .HasNoKey()
                .ToView("ItemTypeMasterView");
            modelBuilder.Entity<Common.PartyMaster>()
                .HasNoKey()
                .ToView("PartyMasterView");
            modelBuilder.Entity<Common.MailerData>()
                .HasNoKey()
                .ToView("MailerDataView");
            modelBuilder.Entity<Common.AccountsLedgerDetails>()
                .HasNoKey()
                .ToView("AccountsLedgerDetailsView");
            modelBuilder.Entity<Common.ComapanyMaster>()
                .HasNoKey()
                .ToView("ComapanyMasterView");
            modelBuilder.Entity<Common.AccountsOutstandingDetails>()
              .HasNoKey()
              .ToView("AccountsOutstandingDetailsView");
            modelBuilder.Entity<Common.AccountsOutstandingSummary>()
              .HasNoKey()
              .ToView("AccountsOutstandingSummaryView");
            modelBuilder.Entity<Common.DispatchDetails>()
              .HasNoKey()
              .ToView("DispatchDetailsView");
        }

        public DbSet<Common.UserDetails> UserDetail { get; set; }
        public DbSet<Common.LoginDetails> LoginDetail { get; set; }
        public DbSet<Common.WebOrderHistoryReport> WebOrderReport { get; set; }
        public DbSet<Common.ItemTypeMaster> ItemType { get; set; }
        public DbSet<Common.DispatchSummary> Dispatchs { get; set; }
        public DbSet<Common.DispatchDetails> DispatachD { get; set; }

        public DbSet<Common.PartyMaster> PartyDetails { get; set; }
        public DbSet<Common.ItemMasterForPlaceOrder> ItemDetailsForPO { get; set; }
        public DbSet<Common.ItemMaster> ItemDetails { get; set; }
        public DbSet<Common.MailerData> MailerDetails { get; set; }
        public DbSet<Common.CartItem> CardItemDetails { get; set; }
        public DbSet<Common.WebOrderConfirm> webOrderDetails { get; set; }
        public DbSet<Common.WebOrderToSo> wotosoDetails { get; set; }
        public DbSet<DPWebAPI.Entities.Common>? Common { get; set; }
        public DbSet<Common.AccountsLedgerDetails> LegderDetails { get; set; }
        public DbSet<Common.ComapanyMaster> Company { get; set; }
        public DbSet<Common.AccountsOutstandingSummary> OutstandingS { get; set; }
        public DbSet<Common.AccountsOutstandingDetails> OutstandingD { get; set; }

        public DbSet<Common.DPPageMaster> DPPage { get; set; }
    }
}
