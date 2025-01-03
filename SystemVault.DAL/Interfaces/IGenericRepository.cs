namespace SystemVault.DAL.Interfaces;

public interface IGenericRepository<T>
{
    Task<T?> GetByIdAsync(long id);
    Task CreateAsync(T entity);
    void Update(T entity);
    Task DeleteAsync(long id);
    Task<bool> EntityExistAsync(long id);
    Task<bool> SaveChangesAsync();
}