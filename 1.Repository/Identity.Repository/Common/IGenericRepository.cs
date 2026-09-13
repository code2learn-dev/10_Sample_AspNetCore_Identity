using Identity.Domain.Common;
using System.Linq.Expressions;
using System.Transactions;

namespace Identity.Repository.Common
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        IEnumerable<TEntity> GetAll();

        Task<IEnumerable<TEntity>> GetAllAsync();

        TEntity? FindById(long id);

        Task<TEntity?> FindByIdAsync(long id);

        Task<TEntity?> AddAsync(TEntity entity);

        Task<TEntity?> UpdateAsync(TEntity entity);

        Task<TEntity?> DeleteAsync(TEntity entity);

        IEnumerable<TEntity> FilterByPredicate(
            Expression<Func<TEntity, bool>>? predicate,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderFilter);

        Task<IEnumerable<TEntity>> FilterByPredicateAsync(
            Expression<Func<TEntity, bool>>? predicate,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderFilter);

        int CommitChanges();

        Task<int> CommitChangesAsync(); 
    }
}
