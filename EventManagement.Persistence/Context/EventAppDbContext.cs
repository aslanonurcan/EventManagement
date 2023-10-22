using EventManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Persistence.Context
{
    public class EventAppDbContext : IdentityDbContext
    {
        public EventAppDbContext(DbContextOptions<EventAppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
    }
}
