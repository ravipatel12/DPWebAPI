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
        }

        public DbSet<Common.UserDetails> UserDetail { get; set; }
        public DbSet<Common.LoginDetails> LoginDetail { get; set; }
        public DbSet<Common.WebOrderHistoryReport> WebOrderReport { get; set; }
        public DbSet<Common.ItemTypeMaster> ItemType { get; set; }
        public DbSet<Common.PartyMaster> PartyDetails { get; set; }
        public DbSet<DPWebAPI.Entities.Common>? Common { get; set; }
    }
}
