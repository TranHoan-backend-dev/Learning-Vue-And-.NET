using DL.Context;
using DL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DL.Repositories;

public class DepartmentRepository(AppDbContext context) : IDepartmentRepository
{
    public async Task<List<Department>> GetAllDepartmentsAsync()
    {
        return await context.Departments
            .ToListAsync();
    }

    public Task<Department?> GetByIdAsync(Guid departmentId)
    {
        return context.Departments
            .FirstOrDefaultAsync(d => d.DepartmentId == departmentId);
    }

    public Task AddAsync(Department department)
    {
        context.Departments.AddAsync(department);
        return context.SaveChangesAsync();
    }

    public Task UpdateAsync(Department department)
    {
        context.Departments.Update(department);
        return context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid departmentId)
    {
        var department = await context.Departments.FindAsync(departmentId);
        if (department != null)
        {
            context.Departments.Remove(department);
            await context.SaveChangesAsync();
        }
    }
}