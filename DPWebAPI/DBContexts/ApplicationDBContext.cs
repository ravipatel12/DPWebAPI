using Microsoft.EntityFrameworkCore;

using DPWebAPI.Entities;

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

        public DbSet<Common.UserDetails> UserDetail { get; set; }
        public DbSet<Common.LoginDetails> LoginDetail { get; set; }

        public DbSet<DPWebAPI.Entities.Common>? Common { get; set; }
    }
}
