using Dapper;
using MISA.Common.Base;

namespace MISA.DL.Base;

public interface IBaseDl<T>
{
    Task<IEnumerable<T>?> GetAllAsync(BaseModel model);
    Task<IEnumerable<T>?> GetPagedAsync(int pageNumber, int pageSize);
    Task<T?> GetByIdAsync(Guid id);
    Task UpdateAsync(T entity, Guid id);
    Task DeleteAsync(T entity, Guid id);
    Task<bool> CheckDuplicate(string propName, Object value);
    Task<object> ExecuteCommandText(string commandText, DynamicParameters parameters);
}