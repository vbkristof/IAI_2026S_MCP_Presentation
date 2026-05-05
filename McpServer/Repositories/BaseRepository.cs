using System.Linq.Expressions;
using McpServer.Data;
using McpServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace McpServer.Repositories;

public abstract class BaseRepository<TEntity, TId>(DataContext dbContext)
    where TEntity : BaseEntity<TId>
    where TId : notnull
{
    public async Task<bool> ExistsByIdAsync(TId id) =>
        await dbContext.Set<TEntity>()
            .AnyAsync(e => e.Id.Equals(id));

    protected async Task<bool> ExistsByExpressionAsync(Expression<Func<TEntity, bool>> expression) =>
        await dbContext.Set<TEntity>()
            .AnyAsync(expression);

    public async Task<IEnumerable<TEntity>> GetAllAsync() =>
        await dbContext.Set<TEntity>()
            .ToListAsync();

    public async Task<IEnumerable<TEntity>> GetAllAsNoTrackingAsync() =>
        await dbContext.Set<TEntity>()
            .AsNoTracking()
            .ToListAsync();

    public async Task<int> CountAsync() =>
        await dbContext.Set<TEntity>()
            .CountAsync();

    public async Task<TEntity> GetByIdAsync(TId id) =>
        await dbContext.Set<TEntity>()
            .FindAsync(id)
        ?? throw new NullReferenceException($"{nameof(TEntity)} not found with Id={id}");

    public async Task<IEnumerable<TEntity>> GetAllByIdsAsync(IEnumerable<TId> ids)
    {
        List<TId> idList = ids.ToList();
        return await dbContext.Set<TEntity>()
            .Where(e => idList.Contains(e.Id))
            .ToListAsync();
    }

    public async Task<TEntity?> GetByExpressionAsync(
        Expression<Func<TEntity, bool>>? filterExpression = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
    {
        IQueryable<TEntity> query = dbContext.Set<TEntity>();
        if (filterExpression != null)
        {
            query = query.Where(filterExpression);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        return await query.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllByExpressionAsync(
        Expression<Func<TEntity, bool>>? filterExpression = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null
    )
    {
        IQueryable<TEntity> query = dbContext.Set<TEntity>();
        if (filterExpression != null)
        {
            query = query.Where(filterExpression);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        return await query.ToListAsync();
    }


    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        EntityEntry<TEntity> added = await dbContext.Set<TEntity>().AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return added.Entity;
    }

    public virtual async Task CreateAllAsync(IEnumerable<TEntity> entities)
    {
        await dbContext.Set<TEntity>().AddRangeAsync(entities);
        await dbContext.SaveChangesAsync();
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        EntityEntry<TEntity> updated = dbContext.Set<TEntity>().Update(entity);
        await dbContext.SaveChangesAsync();
        return updated.Entity;
    }

    public virtual async Task UpdateAllAsync(IEnumerable<TEntity> entities)
    {
        dbContext.Set<TEntity>().UpdateRange(entities);
        await dbContext.SaveChangesAsync();
    }

    public virtual async Task DeleteByIdAsync(TId id) =>
        await dbContext.Set<TEntity>()
            .Where(entity => entity.Id.Equals(id))
            .ExecuteDeleteAsync();

    protected async Task DeleteAllByExpressionAsync(Expression<Func<TEntity, bool>> expression) =>
        await dbContext.Set<TEntity>()
            .Where(expression)
            .ExecuteDeleteAsync();

    public virtual async Task DeleteAllAsync() =>
        await dbContext.Set<TEntity>()
            .ExecuteDeleteAsync();

    public void SetAllNonTracking(IEnumerable<TEntity> entities)
    {
        foreach (TEntity entity in entities)
        {
            dbContext.Entry(entity).State = EntityState.Detached;
        }
    }
}