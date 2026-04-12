using MISA.Common.Base;

namespace MISA.BL.Base;

public interface IBaseBl<T> where T : BaseModel
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    // Task AddAsync(BaseModel model);
    // Task<int> UpdateAsync(BaseModel model, Guid id);
    Task DeleteAsync(T model, Guid id);
    Task<List<T>> SaveDataAsync(List<T> models);
}