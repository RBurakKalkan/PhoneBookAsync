using ContractApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ContractApi.DataLayer
{
    public class ContractsDataContext : DbContext
    {
        public ContractsDataContext(DbContextOptions<ContractsDataContext>options ) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }
        public DbSet<Contracts> Contracts { get; set; }
        public DbSet<ContractsInfo> ContractsInfo { get; set; }
    }
}
