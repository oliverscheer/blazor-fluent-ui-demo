using ConcertDatabase.Contexts;
using ConcertDatabase.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ConcertDatabase.Repositories
{
    public class Repository<TEntity>(IDbContextFactory<MusicDbContext> myDbContextFactory)
        : IRepository<TEntity> where TEntity : Entity
    {
        internal readonly MusicDbContext _context = myDbContextFactory.CreateDbContext();
        protected DbSet<TEntity>? _entities;
        public DbSet<TEntity> Entities
        {
            get
            {
                _entities ??= _context.Set<TEntity>();
                return _entities;
            }
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await Entities.AddAsync(entity);
            return entity;
        }

        public async Task<bool> DeleteAsync(object id)
        {
            var entity = await Entities.FindAsync(id);
            if (entity == null)
            {
                return false;
            }
            return Delete(entity);
        }

        public bool Delete(TEntity entity)
        {
            Entities.Remove(entity);
            return true;
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>>? expression = null)
        {
            var result = Entities.AsQueryable();
            if (expression != null)
            {
                result = result.Where(expression);
            }
            return result;
        }

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            var result = await Entities.FindAsync(id);
            return result;
        }

        public async Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
        {
            var result = await Entities.FirstOrDefaultAsync(expression);
            return result;
        }

        public virtual void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public Task UpdateAsync(object Id, TEntity entity)
        {
            TEntity? exist = Entities.Find(Id);
            if (exist == null)
            {
                return Task.CompletedTask;
            }
            _context.Entry(exist).CurrentValues.SetValues(entity);
            return Task.CompletedTask;
        }

        public async Task SaveAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception exc)
            {
                throw new Exception("Error saving changes", exc);
            }
        }
    }
}
