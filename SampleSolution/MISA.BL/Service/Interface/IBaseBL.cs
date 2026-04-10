using Model.Base;

namespace MISA.BL.Service.Interface;

public interface IBaseBL<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task AddAsync(BaseModel model);
    Task<int> UpdateAsync(BaseModel model, Guid id);
    Task DeleteAsync(BaseModel model, Guid id);
    Task<List<BaseModel>> SaveData(List<BaseModel> models);
}