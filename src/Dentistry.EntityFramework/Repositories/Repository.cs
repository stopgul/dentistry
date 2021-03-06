﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Dentistry.EntityFramework.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DentistryDbContext DbContext;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(DentistryDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<TEntity>();
        }

        public virtual async Task<TEntity> GetByIdAsync(long id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await DbSet.Where(expression).ToListAsync();
        }

        public virtual async Task<int> AddAsync(TEntity obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("entity");
            }
            DbSet.Add(obj);
            return await DbContext.SaveChangesAsync();
        }

        public virtual async Task<int> UpdateAsync(TEntity obj)
        {
            DbSet.Update(obj);
            return await DbContext.SaveChangesAsync();
        }

        public virtual async Task<int> RemoveAsync(long id)
        {
            DbSet.Remove(DbSet.Find(id));
            return await DbContext.SaveChangesAsync();
        }


        //public IQueryable<TEntity> GetAll(
        //    bool condition = false,
        //    Expression<Func<TEntity, bool>> predicate = null,
        //    bool disableTracking = false)
        //{
        //    IQueryable<TEntity> query = DbSet;
        //    if (disableTracking)
        //    {
        //        query = query.AsNoTracking();
        //    }

        //    if (condition && predicate != null)
        //    {
        //        query = query.Where(predicate);
        //    }

        //    return query;
        //}

        //public async Task<TEntity> GetFirstOrDefaultAsync(
        //    Expression<Func<TEntity, bool>> predicate = null,
        //    bool disableTracking = false)
        //{
        //    IQueryable<TEntity> query = DbSet;
        //    if (disableTracking)
        //    {
        //        query = query.AsNoTracking();
        //    }

        //    if (predicate != null)
        //    {
        //        query = query.Where(predicate);
        //    }

        //    return await query.FirstOrDefaultAsync();
        //}

        //public Task InsertAsync(
        //    IEnumerable<TEntity> entities,
        //    CancellationToken cancellationToken = default(CancellationToken))
        //{
        //    return DbSet.AddRangeAsync(entities, cancellationToken);
        //}

        //public Task InsertAsync(
        //    TEntity entity,
        //    CancellationToken cancellationToken = default(CancellationToken))
        //{
        //    return DbSet.AddAsync(entity, cancellationToken);
        //}

        //public void Delete(IEnumerable<TEntity> entities) => DbSet.RemoveRange(entities);

        //public void Insert(TEntity entity)
        //{
        //    DbSet.Add(entity);
        //}

        //public TEntity GetFirstOrDefault(
        //    Expression<Func<TEntity, bool>> predicate = null,
        //    bool disableTracking = false)
        //{
        //    IQueryable<TEntity> query = DbSet;
        //    if (disableTracking)
        //    {
        //        query = query.AsNoTracking();
        //    }

        //    if (predicate != null)
        //    {
        //        query = query.Where(predicate);
        //    }

        //    return query.FirstOrDefault();
        //}
    }
}
