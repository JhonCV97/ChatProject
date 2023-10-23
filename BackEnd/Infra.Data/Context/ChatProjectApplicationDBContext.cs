using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Context
{
    public class ChatProjectApplicationDBContext : DbContext
    {
        public ChatProjectApplicationDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<History> History { get; set; }
        public DbSet<UserHistory> UserHistory { get; set; }
        public DbSet<DataInfo> DataInfo { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Load all assemblies from configurations folder in infra.data
            modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());

        }
    }
}
