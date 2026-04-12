using Microsoft.AspNetCore.Mvc;
using MISA.BL.DTOs.Response;
using MISA.BL.Enums;
using MISA.BL.Service.Impl;
using Model.Base;
using Model.Model;
using Model.Resources;

namespace MISA.API.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class EmployeesController(BaseBL<Employee> baseBl, ILogger<EmployeesController> log) : ControllerBase
    {
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] Employee model, [FromRoute] Guid id)
        {
            try
            {
                // var res = await baseBl.UpdateAsync(model, id);
                model.State = Model.Enum.ModelState.Update;
                var res = await baseBl.SaveData(new List<BaseModel>() {model});
                return Ok(new ApiResponse()
                {
                    Status = (int)AppEnum.StatusCode.Success,
                    Message = ResourcesVN.UpdateSuccessfully,
                    Data = res
                });
            }
            catch (Exception e)
            {
                log.LogError(e, "Error updating entity with id {Id}", id);
                return StatusCode(
                    (int)AppEnum.StatusCode.InternalServerError,
                    new ErrorResult()
                    {
                        DevMsg = e.Message,
                        UserMsg = ResourcesVN.Exception,
                        MoreInfo = e.ToString(),
                    });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddNewEmployee([FromBody] Employee model)
        {
            log.LogInformation("[EmployeesController] Adding new employee");
            // try
            // {
            //     await baseBl.AddAsync(model);
            //     return Ok(new ApiResponse()
            //     {
            //         Status = (int)AppEnum.StatusCode.Success,
            //         Message = "Employee added"
            //     });
            // }
            // catch (Exception e)
            // {
            //     log.LogError(e, e.Message);
            //     return StatusCode(
            //         (int)AppEnum.StatusCode.InternalServerError,
            //         new ErrorResult()
            //         {
            //             DevMsg = e.Message,
            //             UserMsg = ResourcesVN.Exception,
            //             MoreInfo = e.ToString(),
            //         }
            //     );
            // }
            try
            {
                model.State = Model.Enum.ModelState.Insert;
                var res = await baseBl.SaveData(new List<BaseModel>() {model});
                return Ok(new ApiResponse()
                {
                    Status = (int)AppEnum.StatusCode.Success,
                    Message = "Employee added",
                    Data = res
                });
            }
            catch (Exception e)
            {
                log.LogError(e, e.Message);
                return StatusCode(
                    (int)AppEnum.StatusCode.InternalServerError,
                    new ErrorResult()
                    {
                        DevMsg = e.Message,
                        UserMsg = ResourcesVN.Exception,
                        MoreInfo = e.ToString(),
                    }
                );
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            log.LogInformation("[EmployeesController] Deleting employee");
            try
            {
                await baseBl.DeleteAsync(new Employee
                {
                    EmployeeCode = "",
                    FullName = "",
                    Email = ""
                }, id);
                return Ok(new ApiResponse()
                {
                    Status = (int)AppEnum.StatusCode.Success,
                    Message = "Employee added"
                });
            }
            catch (Exception e)
            {
                log.LogError(e, e.Message);
                return StatusCode(
                    (int)AppEnum.StatusCode.InternalServerError,
                    new ErrorResult()
                    {
                        DevMsg = e.Message,
                        UserMsg = ResourcesVN.Exception,
                        MoreInfo = e.ToString(),
                    }
                );
            }
        }

        [HttpPost("SaveMultipleData")]
        public async Task<IActionResult> SaveMultipleData([FromBody] List<BaseModel> models)
        {
            try
            {
                foreach (var item in models)
                {
                    item.State = Model.Enum.ModelState.Insert;
                }

                var res = await baseBl.SaveData(models);
                return Ok(res);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}