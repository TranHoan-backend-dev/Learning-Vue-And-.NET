using System.Data;
using Dapper;
using Microsoft.Extensions.Logging;
using MISA.Common.Base;
using MISA.Common.Procedures;
using MISA.DL.Base;
using MISA.DL.Context;

namespace MISA.DL.Repository;

public class BaseDl<T>(
    DbContext context,
    ILogger<BaseDl<T>> log
) : IBaseDl<T> where T : BaseModel
{
    private const string _LogPrefix = "[BaseDl]";

    public async Task<IEnumerable<T>?> GetAllAsync(BaseModel model)
    {
        log.LogInformation("{prefix} Get all models", _LogPrefix);
        var storedProcedure = string.Format(ProcedureNames.GetAll, model.GetType().Name);
        using var conn = context.GetConnection();
        var res = await conn.QueryAsync<T>(
            storedProcedure,
            commandType: CommandType.StoredProcedure
        );
        log.LogDebug("Response: {res}", res);
        return res;
    }

    public async Task<IEnumerable<T>?> GetPagedAsync(DynamicParameters parameters, string command)
    {
        log.LogInformation("{prefix} Get paginated models list", _LogPrefix);
        using var conn = context.GetConnection();
        var res = await conn.QueryAsync<T>(command, param: parameters);
        log.LogDebug("Response: {res}", res);
        return res;
    }


    public async Task<T?> GetByIdAsync(Guid id)
    {
        log.LogInformation("{prefix} Get model details by id: {id}", _LogPrefix, id.ToString());
        var storedProcedure = string.Format(ProcedureNames.GetDetails, typeof(T).Name);
        using var conn = context.GetConnection();

        var param = new DynamicParameters();
        param.Add("p_Id", id);

        var res = await conn.QueryFirstOrDefaultAsync<T>(
            storedProcedure,
            param,
            commandType: CommandType.StoredProcedure
        );
        log.LogDebug("{prefix} Response: {res}", _LogPrefix, res);
        return res;
    }

    public async Task UpdateAsync(T entity, Guid id)
    {
        log.LogInformation("{prefix} Update model details by id: {id}", _LogPrefix, id.ToString());
        var storedProcedure = string.Format(ProcedureNames.Update, typeof(T).Name);

        var param = new DynamicParameters(entity);
        param.Add("p_Id", id);
        var res = await ExecuteCommandText(storedProcedure, param);
        log.LogDebug("{prefix} Response: {res}", _LogPrefix, res);
    }

    public Task DeleteAsync(T entity, Guid id)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Kiem tra xem 1 obj da ton tai hay chua
    /// </summary>
    /// <param name="propName">Truong du lieu can kiem tra trung lap</param>
    /// <param name="value">Gia tri nghi van la trung lap</param>
    /// <returns>bool</returns>
    public async Task<bool> CheckDuplicate(string propName, object value)
    {
        log.LogInformation("{prefix} Check duplicate", _LogPrefix);
        var storedProcedure = string.Format(ProcedureNames.CheckDuplicate, typeof(T).Name);

        var param = new DynamicParameters();
        param.Add($"p_{propName}", value);

        var res = await ExecuteCommandText(storedProcedure, param);
        log.LogDebug("{prefix} Response: {res}", _LogPrefix, res);
        return (int)res > 0;
    }

    /// <summary>
    /// Build query chi danh cho lenh CUD
    /// </summary>
    /// <param name="commandText"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public async Task<object> ExecuteCommandText(string commandText, DynamicParameters parameters)
    {
        log.LogInformation("{prefix} Build query: {command}", _LogPrefix, commandText);
        using var conn = context.GetConnection();
        var commandType = commandText.Contains(' ') ? CommandType.Text : CommandType.StoredProcedure;
        return await conn.ExecuteAsync(
            commandText,
            parameters,
            commandType: commandType
        );
    }

    public async Task<int> CountTotalElements()
    {
        log.LogInformation($"{_LogPrefix} Count total elements");
        using var conn = context.GetConnection();

        var type = typeof(T);
        var query = $"SELECT COUNT(*) FROM `{type.Name}`";
        return await conn.ExecuteScalarAsync<int>(query);
    }
}