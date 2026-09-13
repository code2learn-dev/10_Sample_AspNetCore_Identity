using Identity.Domain.Common;
using Identity.Domain.IDentityContent;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace Identity.Repository.Common
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly AcademyDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericRepository(AcademyDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual async Task<TEntity?> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            int addedRows = await CommitChangesAsync();
            return addedRows > 0 ? entity : default;
        }

        public int CommitChanges() => _context.SaveChanges();

        public async Task<int> CommitChangesAsync() => await _context.SaveChangesAsync();

        public virtual async Task<TEntity?> DeleteAsync(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
                _context.Entry(entity).State = EntityState.Deleted;

            _dbSet.Remove(entity);
            int deletedRows = await CommitChangesAsync();
            return deletedRows > 0 ? entity : default;
        }

        public virtual IEnumerable<TEntity> FilterByPredicate(Expression<Func<TEntity, bool>>? predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderFilter)
        {
            IQueryable<TEntity> query = _dbSet;
            if(predicate != null)
            {
                query = query.Where(predicate);
            }

            if(orderFilter != null)
            {
                query = orderFilter(query);
            }

            return query.ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> FilterByPredicateAsync(Expression<Func<TEntity, bool>>? predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderFilter)
        {
            IQueryable<TEntity> query = _dbSet;
            if(predicate != null)
            {
                query = query.Where(predicate); 
            }

            if(orderFilter != null)
            {
                query = orderFilter(query);
            }

            return await query.ToListAsync();
        }

        public virtual TEntity? FindById(long id) => _dbSet.Find(id);

        public virtual async Task<TEntity?> FindByIdAsync(long id) => await _dbSet.FindAsync(id);

        public virtual IEnumerable<TEntity> GetAll() => _dbSet.ToList();

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbSet.ToListAsync();

        public virtual async Task<TEntity?> UpdateAsync(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                TEntity? entityToUpdate = await FindByIdAsync(entity.Id);
                if (entityToUpdate is null) return default;

                Type sourceType = entity.GetType();
                Type targetType = entityToUpdate.GetType();

                if (!(Activator.CreateInstance(targetType) is TEntity targetEntity)) return default;

                PropertyInfo[] sourceProps = sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                PropertyInfo[] targetProps = targetType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (var targetProp in targetProps)
                {
                    var sourceProp = sourceProps.SingleOrDefault(a => a.Name.Equals(targetProp.Name, StringComparison.OrdinalIgnoreCase) &&
                                                a.PropertyType == targetProp.PropertyType);
                    if (sourceProp is null) continue;

                    if (sourceProp.Name.Equals("id", StringComparison.OrdinalIgnoreCase)) continue; 

                    var sourcePropValue = sourceProp.GetValue(entity, null);
                    if (sourcePropValue is not null &&
                        sourceProp.CanRead &&
                        targetProp.CanWrite)
                    {
                        targetProp.SetValue(entityToUpdate, sourcePropValue, null);
                    }
                }

                _dbSet.Update(entityToUpdate);
            }
            else
            {
                _dbSet.Update(entity);
            }

            int updatedRows = await CommitChangesAsync();
            return updatedRows > 0 ? entity : default;
        }
    }
}
