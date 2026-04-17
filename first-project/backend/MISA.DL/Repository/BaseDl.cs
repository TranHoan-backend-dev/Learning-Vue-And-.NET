using System.Data;
using Dapper;
using Microsoft.Extensions.Logging;
using MISA.Common.Base;
using MISA.Common.Procedures;
using MISA.DL.Base;
using MISA.DL.Context;

namespace MISA.DL.Repository;

public class BaseDl<TBaseModel>(
    DbContext context,
    ILogger<BaseDl<TBaseModel>> log
) : IBaseDl<TBaseModel> where TBaseModel : BaseModel
{
    private const string LogPrefix = "[BaseDl]";

    public async Task<IEnumerable<TBaseModel>?> GetAllAsync(BaseModel model)
    {
        log.LogInformation("{prefix} Get all models", LogPrefix);
        var storedProcedure = string.Format(ProcedureNames.GetAll, model.GetType().Name);
        using var conn = context.GetConnection();
        var res = await conn.QueryAsync<TBaseModel>(
            storedProcedure,
            commandType: CommandType.StoredProcedure
        );
        log.LogDebug("Response: {res}", res);
        return res;
    }

    public async Task<IEnumerable<TBaseModel>?> GetPagedAsync(DynamicParameters parameters, string command)
    {
        log.LogInformation("{prefix} Get paginated models list", LogPrefix);
        using var conn = context.GetConnection();
        var res = await conn.QueryAsync<TBaseModel>(command, param: parameters);
        log.LogDebug("Response: {res}", res);
        return res;
    }


    public async Task<TBaseModel?> GetByIdAsync(Guid id)
    {
        log.LogInformation("{prefix} Get model details by id: {id}", LogPrefix, id.ToString());
        var storedProcedure = string.Format(ProcedureNames.GetDetails, typeof(TBaseModel).Name);
        using var conn = context.GetConnection();

        var param = new DynamicParameters();
        param.Add("p_Id", id);

        var res = await conn.QueryFirstOrDefaultAsync<TBaseModel>(
            storedProcedure,
            param,
            commandType: CommandType.StoredProcedure
        );
        log.LogDebug("{prefix} Response: {res}", LogPrefix, res);
        return res;
    }

    public async Task UpdateAsync(TBaseModel entity, Guid id)
    {
        log.LogInformation("{prefix} Update model details by id: {id}", LogPrefix, id.ToString());
        var storedProcedure = string.Format(ProcedureNames.Update, typeof(TBaseModel).Name);

        var param = new DynamicParameters(entity);
        param.Add("p_Id", id);
        var res = await ExecuteCommandText(storedProcedure, param);
        // using var conn = context.GetConnection();
        // var res = await conn.ExecuteAsync(
        // storedProcedure,
        // param,
        // commandType: CommandType.StoredProcedure
        // );
        log.LogDebug("{prefix} Response: {res}", LogPrefix, res);
    }

    public Task DeleteAsync(TBaseModel entity, Guid id)
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
        log.LogInformation("{prefix} Check duplicate", LogPrefix);
        var storedProcedure = string.Format(ProcedureNames.CheckDuplicate, typeof(TBaseModel).Name);

        var param = new DynamicParameters();
        param.Add($"p_{propName}", value);

        // using var conn = context.GetConnection();
        // var res = await conn.ExecuteAsync(
        // storedProcedure,
        // param,
        // commandType: CommandType.StoredProcedure
        // );
        var res = await ExecuteCommandText(storedProcedure, param);
        log.LogDebug("{prefix} Response: {res}", LogPrefix, res);
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
        log.LogInformation("{prefix} Build query: {command}", LogPrefix, commandText);
        using var conn = context.GetConnection();
        var commandType = commandText.Contains(' ') ? CommandType.Text : CommandType.StoredProcedure;
        return await conn.ExecuteAsync(
            commandText,
            parameters,
            commandType: commandType
        );
    }
}