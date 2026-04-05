using DL.Context;
using DL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DL.Repositories;

public class EmployeeRepository(AppDbContext context) : IEmployeeRepository
{
    public async Task<List<Employee>> GetAllAsync()
    {
        return await context.Employees
            .Include(e => e.Department)
            .ToListAsync();
    }

    public Task<Employee?> GetByIdAsync(Guid employeeId)
    {
        return context.Employees
            .Include(e => e.Department)
            .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
    }

    public async Task AddAsync(Employee employee)
    {
        context.Employees.Add(employee);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Employee employee)
    {
        context.Employees.Update(employee);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid employeeId)
    {
        var employee = context.Employees
            .FirstOrDefault(e => e.EmployeeId == employeeId);
        if (employee != null)
        {
            context.Employees.Remove(employee);
            await context.SaveChangesAsync();
        }
    }
}