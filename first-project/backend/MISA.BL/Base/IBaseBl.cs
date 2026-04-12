using MISA.Common.Base;

namespace MISA.BL.Base;

public interface IBaseBl<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    // Task AddAsync(BaseModel model);
    // Task<int> UpdateAsync(BaseModel model, Guid id);
    Task DeleteAsync(BaseModel model, Guid id);
    Task<List<BaseModel>> SaveDataAsync(List<BaseModel> models);
}