using DL.Repository.Impl;
using Microsoft.Extensions.Logging;
using Model.Base;
using Model.Model;

namespace MISA.BL.Service.Impl;

public class BaseBlEmployee(BaseDL baseDl, ILogger<BaseBL<Employee>> log) : BaseBL<Employee>(baseDl, log)
{
    // public override bool IsMale(Employee employee)
    // {
    //     
    // }
    public override async Task BeforeSave(List<BaseModel> models)
    {
        await base.BeforeSave(models);

        if (models?.Count > 0 && models[0] is Employee)
        {
            List<Employee> employees = models.Cast<Employee>().ToList();
            foreach (var e in employees)
            {
                e.FullName = $"{e.FullName}_APU";
            }
        }
    }
}