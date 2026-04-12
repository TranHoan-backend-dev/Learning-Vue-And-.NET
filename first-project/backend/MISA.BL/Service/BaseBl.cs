using System.Text;
using Dapper;
using Microsoft.Extensions.Logging;
using MISA.BL.Base;
using MISA.Common.Base;
using MISA.Common.Enum;
using MISA.Common.Extension;
using MISA.DL.Base;

namespace MISA.BL.Service;

public sealed class BaseBl<T>(
    IBaseDl<T> baseDl,
    ILogger<BaseBl<T>> log
) : IBaseBl<T> where T : BaseModel
{
    private readonly string _logPrefix = "[BaseBl]";

    public Task<IEnumerable<T>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<T?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(T model, Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<T>> SaveDataAsync(List<T> models)
    {
        log.LogInformation("{prefix} Save data", _logPrefix);
        if (models.Count > 0)
        {
            await BeforeSave(models);

            var rowInsert = models.Where(m =>
                    m.State == AppEnum.ModelState.Insert || m.State == AppEnum.ModelState.Update)
                .ToList();

            var param = new DynamicParameters();
            var command = BuildQueryStringInsertAndUpdate(rowInsert, ref param);
            log.LogDebug("{prefix} Build command: {command}", _logPrefix, command);

            var result = await baseDl.ExecuteCommandText(command, param);
            log.LogDebug("{prefix} Output: {res}", _logPrefix, result);
            return models;
        }

        return [];
    }

    private String BuildQueryStringInsertAndUpdate(List<T> models, ref DynamicParameters parameters)
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

            var parameterNames = columns.Select(col => $"@{col}_{count}");
            var s = string.Join(",", parameterNames);

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

            count++;
        }

        log.LogInformation("{prefix} Append update command", _logPrefix);
        var columnUpdate = columns
            .FindAll(c => c != primaryKey)
            .Select(c => $"`{c}` = VALUES(`{c}`)");
        sql.AppendLine($"ON DUPLICATE KEY UPDATE {string.Join(", ", columnUpdate)}");
        return sql.ToString();
    }

    private Task BeforeSave(List<T> models)
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