using BL.Service;
using DL.Interfaces;

namespace BL.Impl;

public class EmployeeService(IEmployeeRepository repository) : IEmployeeService
{
    public void CreateNewEmployee()
    {
        
    }
}