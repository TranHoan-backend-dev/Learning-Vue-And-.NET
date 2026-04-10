using Dapper;
using Model.Base;

namespace DL.Repository.Base
{
    public interface IBaseDL<T>
    {
        Task<IEnumerable<T>?> GetAllAsync();

        // TODO: Pageable
        Task<IEnumerable<T>?> GetPagedAsync(int pageNumber, int pageSize);
        Task<T?> GetByIdAsync(Guid id);
        Task AddAsync(T entity);
        Task<int> UpdateAsync(T entity, Guid id);
        Task DeleteAsync(T entity, Guid id);

        Task<bool> CheckDuplicate(string propName, Object value);
        Task<object> ExecuteCommandText(string commandText, DynamicParameters param);
    }
}