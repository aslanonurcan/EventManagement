using EventManagement.Application.Interfaces;
using EventManagement.Domain.Common;
using EventManagement.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private readonly EventAppDbContext _dbContext;
        private IDbContextTransaction _dbTransaction;

        public Repository(EventAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private DbSet<T> Table { get => _dbContext.Set<T>(); }

        public void Add(T entity)
        {
            entity.CreatedAt = DateTime.Now;
            ExecuteTransaction(() =>
            {
                Table.Add(entity);
            });
        }

        public void Delete(int id)
        {
            var entity = Table.Find(id);

            if (entity != null)
            {
                entity.IsDeleted = true;
                Update(entity);
            }
        }

        private IQueryable<T> GetQueryable(bool isDeleted = false)
        {
            return Table.AsNoTracking().Where(s => s.IsDeleted == isDeleted);
        }

        public IEnumerable<T> Filter(Expression<Func<T, bool>> filter, bool isDeleted = false)
        {
            return GetQueryable(isDeleted).Where(filter).ToList();
        }

        public IEnumerable<T> GetAll(bool isDeleted = false)
        {
            return GetQueryable(isDeleted).ToList();
        }

        public T GetById(int id, bool isDeleted = false)
        {
            return GetQueryable(isDeleted).FirstOrDefault(s => s.Id == id);
        }

        public void Update(T entity)
        {
            ExecuteTransaction(() =>
            {
                Table.Attach(entity);
                _dbContext.Entry(entity).State = EntityState.Modified;
            });
        }


        private void ExecuteTransaction(Action action)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    action();
                    _dbContext.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
        }
    }
}
