using Microsoft.EntityFrameworkCore;
using RaportGenerator.DataAccess.Entities;

namespace RaportGenerator.DataAccess
{
    public class RaportGeneratorContext : DbContext
    {
        public RaportGeneratorContext(DbContextOptions<RaportGeneratorContext> options) : base(options)
        {
        }
        public DbSet<Company> Companies { get; set; }
    }
}
