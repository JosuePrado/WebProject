namespace WebProject.Repositories.Interfaces;

public interface IBaseRepository<T>
{
    Task<T?> Add(T item);
    Task<T?> UpdateAsync(T item);
    Task<T?> GetById(int id);
    Task<IEnumerable<T>> GetAll();
    Task<bool> Delete(T item);
}