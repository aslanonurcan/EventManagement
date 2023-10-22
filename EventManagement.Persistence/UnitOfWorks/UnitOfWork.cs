using EventManagement.Application.Interfaces;
using EventManagement.Domain.Common;
using EventManagement.Domain.Entities;
using EventManagement.Persistence.Context;
using EventManagement.Persistence.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace EventManagement.Persistence.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EventAppDbContext _dbContext;

        public IEventRepository EventRepository { get; }

        public UnitOfWork(EventAppDbContext dbContext)
        {
            _dbContext = dbContext;
            EventRepository = new EventRepository(_dbContext);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
