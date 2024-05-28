using Microsoft.EntityFrameworkCore;

namespace asd123.Services
{
    public interface IBaseService<T> where T : class
    {
        IQueryable<T> GetInstance();
        T Create(T entity);
        Task<T> CreateAsync(T entity);
        List<T> Creates(List<T> entities);
        Task<List<T>> CreatesAsync(List<T> entities);
        List<T> FindAll();
        Task<List<T>> FindAllAsync();
        T FindOne(int id);
        Task<T> FindOneAsync(int id);
        T Update(T entity);
        Task<T> UpdateAsync(T entity);
        T Delete(int id);
        Task<T> DeleteAsync(int id);
        List<T> Deletes(int[] ids);
        Task<List<T>> DeletesAsync(int[] ids);
    }

    public abstract class BaseService<TEntity, TContext> : IBaseService<TEntity>
       where TEntity : class
       where TContext : DbContext
    {
        private readonly TContext context;

        public BaseService(TContext context)
        {
            this.context = context;
        }

        public TEntity Create(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            context.SaveChanges();
            return entity;
        }
        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public List<TEntity> Creates(List<TEntity> entities)
        {
            context.Set<TEntity>().AddRange(entities);
            context.SaveChanges();
            return entities;
        }

        public async Task<List<TEntity>> CreatesAsync(List<TEntity> entities)
        {
            context.Set<TEntity>().AddRange(entities);
            await context.SaveChangesAsync();
            return entities;
        }

        public List<TEntity> FindAll()
        {
            return context.Set<TEntity>().ToList();
        }

        public async Task<List<TEntity>> FindAllAsync()
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        public TEntity FindOne(int id)
        {
            return context.Set<TEntity>().Find(id);
        }
        public async Task<TEntity> FindOneAsync(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public TEntity Update(TEntity entity)
        {
            context.Entry(entity).CurrentValues.SetValues(entity);
            context.SaveChanges();
            return entity;
        }
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entity;
        }

        public TEntity Delete(int id)
        {
            var entity = context.Set<TEntity>().Find(id);
            if (entity == null)
            {
                return entity;
            }
            context.Set<TEntity>().Remove(entity);
            context.SaveChanges();
            return entity;
        }
        public async Task<TEntity> DeleteAsync(int id)
        {
            var entity = await context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }
            context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public List<TEntity> Deletes(int[] ids)
        {
            var query = context.Set<TEntity>().AsQueryable();
            var selectedItems = query.Where(item => ids.Contains((int)item.GetType().GetProperty("Id").GetValue(item))).ToList();
            context.Set<TEntity>().RemoveRange(selectedItems);
            context.SaveChanges();
            return selectedItems;
        }

        public async Task<List<TEntity>> DeletesAsync(int[] ids)
        {
            var query = context.Set<TEntity>().AsQueryable();
            var selectedItems = await query.Where(item => ids.Contains((int)item.GetType().GetProperty("Id").GetValue(item))).ToListAsync();
            context.Set<TEntity>().RemoveRange(selectedItems);
            await context.SaveChangesAsync();
            return selectedItems;
        }

        public IQueryable<TEntity> GetInstance()
        {
            return context.Set<TEntity>().AsQueryable();
        }
    }
}
