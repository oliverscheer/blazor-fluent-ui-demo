using System.Linq.Expressions;

namespace ConcertDatabase.Repositories
{
    public interface IRepository<TEntity> 
    {
        Task<TEntity> AddAsync(TEntity entity);
        bool Delete(TEntity entity);
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>>? expression = null);
        Task<TEntity?> GetByIdAsync(Guid id);
        void Update(TEntity entity);
        Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression);
        Task SaveAsync();
    }
}
