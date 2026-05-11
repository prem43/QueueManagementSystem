using System.Linq.Expressions;

namespace QueueManagement.Application.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);

    Task<IEnumerable<T>> GetAllAsync();

    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);

    Task AddAsync(T entity);

    void Update(T entity);
    Task<IEnumerable<T>> GetAllIncludingAsync(Expression<Func<T, bool>>? filter = null,params string[] includes);
    void Remove(T entity);
}