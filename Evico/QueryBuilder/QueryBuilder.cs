using System.Linq.Expressions;
using Evico.Entity;
using Microsoft.EntityFrameworkCore;

namespace Evico.QueryBuilder;

public abstract class QueryBuilder<TEntity, TContext>
    : IDisposable where TEntity : class, IEntity, new()
    where TContext : DbContext
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected static IQueryable<TEntity> Query;
#pragma warning restore CS8618

    protected QueryBuilder(TContext context)
    {
        Context = context;
        Query = Context.Set<TEntity>();
    }

    protected TContext Context { get; set; }

    public void Dispose()
    {
        Context.Dispose();
    }

    public static implicit operator List<TEntity>(QueryBuilder<TEntity, TContext> queryBuilder)
    {
        return Query.ToList();
    }

    public static implicit operator TEntity?(QueryBuilder<TEntity, TContext> queryBuilder)
    {
        return Query.FirstOrDefault();
    }

    public List<TEntity> ToList()
    {
        return Query.ToList();
    }

    public async Task<List<TEntity>> ToListAsync()
    {
        return await Query.ToListAsync();
    }

    public QueryBuilder<TEntity, TContext> Join<TTargeIEntity, TKey>(Expression<Func<TEntity, TKey>> tKey,
        Expression<Func<TTargeIEntity, TKey>> uKey) where TTargeIEntity : class
    {
        Query = Query.Join(Context.Set<TTargeIEntity>(), tKey, uKey, (tblT, tblU) => tblT);
        return this;
    }

    public QueryBuilder<TEntity, TContext> JoinWithPredicate<TTargeIEntity, TKey>(Expression<Func<TEntity, TKey>> tKey,
        Expression<Func<TTargeIEntity, TKey>> uKey,
        params Expression<Func<TTargeIEntity, bool>>[]? whereExpressions) where TTargeIEntity : class
    {
        if (whereExpressions == null) return this;

        var targetSets = Context.Set<TTargeIEntity>().AsQueryable();
        targetSets = whereExpressions.Aggregate(targetSets, (current, expression) => current.Where(expression));

        Query = Query.Join(targetSets, tKey, uKey, (tblT, tblU) => tblT);

        return this;
    }

    public QueryBuilder<TEntity, TContext> Include(params Expression<Func<TEntity, object>>[] includeExpressions)
    {
        foreach (var includeExpression in includeExpressions) 
            Query = Query.Include(includeExpression);

        return this;
    }

    public QueryBuilder<TEntity, TContext> Skip(int count)
    {
        Query = Query.Skip(count);

        return this;
    }

    public QueryBuilder<TEntity, TContext> Limit(int count)
    {
        Query = Query.Take(count);

        return this;
    }

    public QueryBuilder<TEntity, TContext> WithId(long entityId)
    {
        Query = Query.Where(e => e.Id == entityId);

        return this;
    }

    public QueryBuilder<TEntity, TContext> WithIds(List<long> entityIds)
    {
        Query = Query.Where(e => entityIds.Contains(e.Id));

        return this;
    }

    public QueryBuilder<TEntity, TContext> WhereDeleted()
    {
        Query = Query.Where(e => e.IsDeleted);

        return this;
    }

    public QueryBuilder<TEntity, TContext> WhereNotDeleted()
    {
        Query = Query.Where(e => !e.IsDeleted);

        return this;
    }


    public QueryBuilder<TEntity, TContext> Paginate(Pagination pagination)
    {
        var totalCount = Query.Count();

        return Skip(pagination.GetOffset(totalCount))
            .Limit(pagination.ItemsPerPage);
    }

    public TEntity? FirstOrDefault()
    {
        return Query.FirstOrDefault();
    }

    public async Task<TEntity?> FirstOrDefaultAsync()
    {
        return await Query.FirstOrDefaultAsync();
    }

    public TEntity Single()
    {
        return Query.Single();
    }

    public async Task<TEntity> SingleAsync()
    {
        return await Query.SingleAsync();
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        Context.Update(entity);
        Context.Entry(entity).State = EntityState.Modified;
        await Context.SaveChangesAsync();

        return entity;
    }

    public async Task<List<TEntity>> UpdateAsync(List<TEntity> entities)
    {
        Context.UpdateRange(entities);
        await Context.SaveChangesAsync();

        return entities;
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        Context.Add(entity);
        Context.Entry(entity).State = EntityState.Added;
        await Context.SaveChangesAsync();

        return entity;
    }

    public async Task<List<TEntity>> AddAsync(List<TEntity> entity)
    {
        await Context.AddRangeAsync(entity);
        await Context.SaveChangesAsync();

        return entity;
    }

    public async Task<TEntity> DeleteAsync(TEntity entity)
    {
        entity.IsDeleted = true;
        entity.DeletedDateTime = DateTime.UtcNow;

        return await UpdateAsync(entity);
    }

    public async Task<List<TEntity>> DeleteAsync(List<TEntity> entities)
    {
        return await UpdateAsync(entities.Select(entity =>
        {
            entity.IsDeleted = true;
            entity.DeletedDateTime = DateTime.UtcNow;
            return entity;
        }).ToList());
    }

    public async Task<List<TEntity>> DeleteAsync()
    {
        return await DeleteAsync(await ToListAsync());
    }

    public async Task<TEntity> UndoDeleteAsync(TEntity entity)
    {
        entity.IsDeleted = false;
        entity.DeletedDateTime = null;

        return await UpdateAsync(entity);
    }

    public async Task<List<TEntity>> UndoDeleteAsync(List<TEntity> entities)
    {
        return await UpdateAsync(entities.Select(entity =>
        {
            entity.IsDeleted = false;
            entity.DeletedDateTime = null;
            return entity;
        }).ToList());
    }

    public async Task<List<TEntity>> UndoDeleteAsync()
    {
        return await UndoDeleteAsync(await ToListAsync());
    }
}