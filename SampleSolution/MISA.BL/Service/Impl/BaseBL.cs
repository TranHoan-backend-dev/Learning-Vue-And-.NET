using System.Text;
using Dapper;
using DL.Repository.Impl;
using Microsoft.Extensions.Logging;
using MISA.BL.Service.Interface;
using Model.Attributes;
using Model.Base;
using Model.Enum;
using Model.Exception;
using Model.Extension;

namespace MISA.BL.Service.Impl;

public class BaseBL<T>(BaseDL baseDl, ILogger<BaseBL<T>> log) : IBaseBL<BaseModel>
{
    public Task<IEnumerable<BaseModel>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<BaseModel?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(BaseModel model)
    {
        log.LogInformation("Add new model {model}", model);
        await ValidateDuplicate(model);
        await baseDl.AddAsync(model);
    }

    public async Task<int> UpdateAsync(BaseModel model, Guid id)
    {
        log.LogInformation("[{BaseBL}] Update model {model}", GetType().Name, model);
        await CheckExists(model, id);
        return await baseDl.UpdateAsync(model, id);
    }

    public async Task DeleteAsync(BaseModel model, Guid id)
    {
        log.LogInformation("[{BaseBL}] Delete model {model}", GetType().Name, model);
        await CheckExists(model, id);
        await baseDl.DeleteAsync(model, id);
    }

    public async Task<List<BaseModel>> SaveData(List<BaseModel> models)
    {
        log.LogInformation("[{BaseBL}] Save data {models}]", GetType().Name, models);
        if (models.Count > 0)
        {
            await BeforeSave(models);

            var rowInsert = models.Where(s => s.State == ModelState.Insert || s.State == ModelState.Update).ToList();
            var rowDelete = models.Where(s => s.State == ModelState.Delete).ToList();

            var param = new DynamicParameters();
            var command = BuildQueryStringInsertUpdate(rowInsert, ref param);
            
            var result = await baseDl.ExecuteCommandText(command, param);
        }

        return new List<BaseModel>();
    }

    /// <summary>
    /// Build logic CUD
    /// </summary>
    /// <returns></returns>
    public String BuildQueryStringInsertUpdate(List<BaseModel> models, ref DynamicParameters param)
    {
        StringBuilder sql = new StringBuilder();
        var tableName = models[0].GetType().GetTableNameOnly();
        var primaryKey = models[0].GetType().GetPrimaryKeyType();
        var columns = models[0].GetType().GetAllColumns();

        sql.Append($"INSERT INTO `{tableName}` ({string.Join(",", columns)}) VALUES");

        var isFirst = true;
        var count = 0;
        foreach (var model in models)
        {
            Guid.TryParse(model.GetValue(primaryKey) + "", out Guid keyValue);
            if (keyValue == null || keyValue == Guid.Empty)
            {
                keyValue = Guid.NewGuid();
            }
            if (isFirst)
            {
                sql.Append($"({"@" + string.Join($"_{count++},@", columns)}_{count})");
                isFirst = false;
            }
            else
            {
                sql.Append($", ({"@" + string.Join($"_{count++},@", columns)}_{count})");
            }

            foreach (var col in columns)
            {
                if (primaryKey == col)
                {
                    param.Add($"@{col}_{count}", keyValue);
                }
                else
                {
                    param.Add($"@{col}_{count}", model.GetValue(col));
                }
            }
        }

        var columnUpdate = columns.FindAll(x => x != primaryKey)
            .Select(x => $"`{x}` = VALUES(`{x}`)");
        sql.AppendLine($"ON DUPLICATE KEY UPDATE {string.Join(", ", columnUpdate)}");
        return sql.ToString();
    }

    public virtual async Task BeforeSave(List<BaseModel> models)
    {
        foreach (var item in models)
        {
            item.CreateBy = "Admin";
            item.CreatedDate = DateTime.Now;
            item.ModifiedBy = "Admin";
            item.ModifiedDate = DateTime.Now;
        }
    }

    protected virtual bool IsMale(BaseModel model)
    {
        return true;
    }

    // /// <summary>
    // /// Validate nghiep vu them moi
    // /// </summary>
    // /// <param name="model"></param>
    // /// <returns></returns>
    // private async Task<bool> ValidateBusiness(BaseModel model)
    // {
    //     log.LogInformation("Validating model");
    //     await ValidateDuplicate(model);
    //
    //     return true;
    // }

    /// <summary>
    /// Validate nghiep vu them moi
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    private async Task ValidateDuplicate(BaseModel model)
    {
        log.LogInformation("Validate duplicated model");
        // lay danh sach cac thuoc tinh cua lop con
        var listProperties = model.GetType().GetProperties();
        foreach (var item in listProperties)
        {
            // kiem tra xem thuoc tinh co Attribute [CheckDuplicate] hay khong
            // neu khong co thi bo qua, duyet den thuoc tinh tiep theo
            if (Attribute.GetCustomAttribute(item, typeof(CheckDuplicateAttribute)) is not CheckDuplicateAttribute attr)
            {
                continue;
            }

            // lay gia tri ra de check trung
            var value = item.GetValue(model)!;

            // xu ly trong DB
            var res = await baseDl.CheckDuplicate(item.Name, value);
            if (res)
            {
                log.LogError("{model} already exists", value);
                throw new ExistingException(attr.ErrorMessage);
            }
        }
    }

    private async Task CheckExists(BaseModel model, Guid id)
    {
        log.LogInformation("CheckExists model {model}", model);
        var res = await baseDl.CheckModelExists(model, id.ToString());
        if (!res)
        {
            throw new ExistingException($"{model.GetType()} with id {id.ToString()} is Existing");
        }
    }
}