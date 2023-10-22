using EventManagement.Application.Interfaces;
using EventManagement.Domain.Entities;
using EventManagement.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Persistence.Repositories
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        public EventRepository(EventAppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
