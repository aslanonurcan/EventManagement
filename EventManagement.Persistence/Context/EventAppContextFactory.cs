using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EventManagement.Persistence.Context
{
    public class EventAppContextFactory : IDesignTimeDbContextFactory<EventAppDbContext>
    {
        public EventAppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EventAppDbContext>();

            optionsBuilder.UseNpgsql("User ID = postgres;Password=123456;Server=localhost;Port=5432;Database=EventAppDatabase;Integrated Security=true;");

            return new EventAppDbContext(optionsBuilder.Options);
        }
    }
}
