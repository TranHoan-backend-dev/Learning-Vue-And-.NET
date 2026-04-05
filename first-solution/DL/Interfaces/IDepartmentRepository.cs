using Model;

namespace DL.Interfaces;

public interface IDepartmentRepository
{
    Task<List<Department>> GetAllDepartmentsAsync();
    Task<Department?> GetByIdAsync(Guid departmentId);
    Task AddAsync(Department department);
    Task UpdateAsync(Department department);
    Task DeleteAsync(Guid departmentId);
}