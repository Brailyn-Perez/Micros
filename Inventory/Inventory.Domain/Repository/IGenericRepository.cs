namespace Inventory.Domain.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
        Task<int> AddAsync(T entity, CancellationToken cancellationToken);
        Task<int> UpdateAsync(T entity, CancellationToken cancellationToken);
        Task<int> DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
