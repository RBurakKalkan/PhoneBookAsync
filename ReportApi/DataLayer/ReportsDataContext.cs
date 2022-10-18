using Microsoft.EntityFrameworkCore;
using ReportApi.Models;

namespace ReportApi.DataLayer
{
    public class ReportsDataContext : DbContext
    {
        public ReportsDataContext(DbContextOptions<ReportsDataContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }
        public DbSet<Reports> Reports { get; set; }
    }
}
