using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RaportGenerator.DataAccess
{
    public class RaportGeneratorContextFactory : IDesignTimeDbContextFactory<RaportGeneratorContext>

    {
        public RaportGeneratorContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RaportGeneratorContext>();
            optionsBuilder.UseSqlServer(@"Data Source =.\; Initial Catalog = RaportGenerator; Integrated Security = True)");
            return new RaportGeneratorContext(optionsBuilder.Options);
        }
    }
}
