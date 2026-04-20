using MISA.BL.DTO.Request;
using MISA.Common.Base;
using MISA.Common.Model;

namespace MISA.BL.Base;

public interface IBaseBl<T> where T : BaseModel
{
    Task<IEnumerable<T>?> GetAllAsync(Pageable pageable, FilterRequest request);
    Task<T?> GetByIdAsync(Guid id);
    // Task AddAsync(BaseModel model);
    // Task<int> UpdateAsync(BaseModel model, Guid id);
    Task DeleteAsync(T model, Guid id);
    Task<List<T>> SaveDataAsync(List<T> models);

    Task<int> CountTotalElements();
}