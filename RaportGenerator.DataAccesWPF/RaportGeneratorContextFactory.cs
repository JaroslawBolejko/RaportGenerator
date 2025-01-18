using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RaportGenerator.DataAccess
{
    public class RaportGeneratorContextFactory : IDesignTimeDbContextFactory<RaportGeneratorContext>
    {
        string connectionStringLocal = "Data Source=LAPTOP-1A0N2URC\\SQLEXPRESS01;Initial Catalog=RaportGeneratorDB;Integrated Security=True;TrustServerCertificate=True";

        public RaportGeneratorContext CreateDbContext(string[] args)
        {
            var optionBuilder = new DbContextOptionsBuilder<RaportGeneratorContext>();
            optionBuilder.UseSqlServer(connectionStringLocal);
            return new RaportGeneratorContext(optionBuilder.Options);
        }
    }
}
