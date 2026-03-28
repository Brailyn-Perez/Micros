using Inventory.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Inventory.infrastructure.Repositories.Base
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly InventoryDbContext _context;

        public GenericRepository(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(T entity, CancellationToken cancellationToken)
        {
            _context.Set<T>().Add(entity);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _context.Set<T>().FindAsync(new object[] { id }, cancellationToken);
            if (entity == null)
            {
                return 0;
            }

            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Set<T>().ToListAsync(cancellationToken);
        }

        public Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            _context.Set<T>().Update(entity);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
