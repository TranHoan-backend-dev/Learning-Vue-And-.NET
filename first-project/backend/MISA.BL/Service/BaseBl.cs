using System.Text;
using Dapper;
using Microsoft.Extensions.Logging;
using MISA.BL.Base;
using MISA.Common.Base;
using MISA.Common.Enum;
using MISA.Common.Extension;
using MISA.DL.Base;

namespace MISA.BL.Service;

public class BaseBl(
    IBaseDl<BaseModel> baseDl,
    ILogger<BaseBl> log
) : IBaseBl<BaseModel>
{
    private readonly string _logPrefix = "[BaseBl]";

    public Task<IEnumerable<BaseModel>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<BaseModel?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(BaseModel model, Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<BaseModel>> SaveDataAsync(List<BaseModel> models)
    {
        log.LogInformation("{prefix} Save data", _logPrefix);
        if (models.Count > 0)
        {
            await BeforeSave(models);

            var rowInsert = models.Where(m =>
                    m.State == AppEnum.ModelState.Insert || m.State == AppEnum.ModelState.Update)
                .ToList();
            // var rowDelete = models.Where(m => m.State == AppEnum.ModelState.Delete)
            // .ToList();

            var param = new DynamicParameters();
            var command = BuildQueryStringInsertAndUpdate(rowInsert, ref param);
            log.LogDebug("{prefix} Build command: {command}", _logPrefix, command);

            var result = await baseDl.ExecuteCommandText(command, param);
            log.LogDebug("{prefix} Output: {res}", _logPrefix, result);
            return (List<BaseModel>)result;
        }

        return new List<BaseModel>();
    }

    private String BuildQueryStringInsertAndUpdate(List<BaseModel> models, ref DynamicParameters parameters)
    {
        var sql = new StringBuilder();
        var type = models[0].GetType();

        var tableName = type.GetTableNameOnly();
        var primaryKey = type.GetPrimaryKeyType();
        var columns = type.GetAllColumns();

        var columnsList = string.Join(",", columns);
        sql.Append($"INSERT INTO `{tableName}` ({columnsList}) VALUES");

        var isFirst = true;
        var count = 0;
        foreach (var model in models)
        {
            // neu parse fail hoac keyValue bi empty thi tao key moi
            if (!Guid.TryParse(model.GetValue(primaryKey) + "", out Guid keyValue) || keyValue == Guid.Empty)
            {
                keyValue = Guid.NewGuid();
            }

            var s = "@" + string.Join($"_{count++},@", columns) + $"_{count}";
            log.LogDebug("{prefix} Append sql command: {s}", _logPrefix, s);
            if (isFirst)
            {
                sql.Append($"({s})");
                isFirst = false;
            }
            else
            {
                sql.Append($", ({s})");
            }

            foreach (var col in columns)
            {
                var value = primaryKey == col ? keyValue : model.GetValue(col);
                parameters.Add($"@{col}_{count}", value);
            }
        }

        log.LogInformation("{prefix} Append update command", _logPrefix);
        var columnUpdate = columns
            .FindAll(c => c != primaryKey)
            .Select(c => $"`{c}` = VALUES(`{c}`)");
        sql.AppendLine($"ON DUPLICATE KEY UPDATE {string.Join(", ", columnUpdate)}");
        return sql.ToString();
    }

    protected virtual Task BeforeSave(List<BaseModel> models)
    {
        foreach (var item in models)
        {
            item.CreatedBy = "Admin";
            item.CreatedAt = DateTime.Now;
            item.ModifiedBy = "Admin";
            item.ModifiedAt = DateTime.Now;
        }

        return Task.CompletedTask;
    }
}