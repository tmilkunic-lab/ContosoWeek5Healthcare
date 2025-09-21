using Microsoft.EntityFrameworkCore;
using ContosoWeek5Healthcare.Models;

namespace ContosoWeek5Healthcare.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }

        public DbSet<SupplyItem> SupplyItems => Set<SupplyItem>();
    }
}
