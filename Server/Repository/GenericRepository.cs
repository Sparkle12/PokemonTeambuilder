using Microsoft.EntityFrameworkCore;
using SharedLibrary;

namespace Server.Repository
{
    public class GenericRepository<T> where T : BaseClass
    {
        protected readonly GameDbContext _context;
        protected readonly DbSet<T> _table;

        public GenericRepository (GameDbContext context)
            {
                _context = context;
                _table = context.Set<T>();
            }

        public async Task<List<T>> GetAll()
        {
            return await _table.AsNoTracking().ToListAsync();
        }


        // Create
        public void Create(T entity)
        {
            _table.Add(entity);
        }

        public async Task CreateAsync(T entity)
        {
            await _table.AddAsync(entity);
        }

        public void CreateRange(IEnumerable<T> entities)
        {
            _table.AddRange(entities);
        }

        public async Task CreateRangeAsync(IEnumerable<T> entities)
        {
            await _table.AddRangeAsync(entities);
        }

        // Update

        public void Update(T entity)
        {
            _table.Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _table.UpdateRange(entities);
        }

        // Delete

        public void Delete(T entity)
        {
            _table.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _table.RemoveRange(entities);
        }

        // Find
        public virtual T FindById(int id)
        {
            return _table.Find(id);
        }

        public virtual async Task<T> FindByIdAsync(int id)
        {
            return await _table.FindAsync(id);
        }

        // Save
        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
