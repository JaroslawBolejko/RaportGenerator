using Microsoft.EntityFrameworkCore;
using RaportGenerator.DataAccess.Entities;

namespace RaportGenerator.DataAccess
{
    public class RaportGeneratorContext : DbContext
    {
        public RaportGeneratorContext()
        {
        }

        public RaportGeneratorContext(DbContextOptions<RaportGeneratorContext> options) : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ContractDevice> ContractDevices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=LAPTOP-1A0N2URC\\SQLEXPRESS01; Database=RaportGeneratorDB; Trusted_Connection=True; TrustServerCertificate=True");
            }
        }
    }
}
