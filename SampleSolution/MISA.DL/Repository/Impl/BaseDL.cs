using System.Data;
using Dapper;
using DL.Context;
using DL.Repository.Base;
using Microsoft.Extensions.Logging;
using Model.Base;
using Model.Model;
using Model.Procedures;

namespace DL.Repository.Impl;

public class BaseDL(
    ILogger<BaseDL> log,
    DbContext context
) : IBaseDL<BaseModel>
{
    public Task<IEnumerable<BaseModel>?> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<BaseModel>?> GetPagedAsync(int pageNumber, int pageSize)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseModel?> GetByIdAsync(Guid id)
    {
        log.LogInformation("[{BaseDL}] GetByIdAsync with id: {id}", nameof(BaseModel), id);
        var storedProcedure = string.Format(ProcedureName.GetById, nameof(BaseModel));
        var param = new DynamicParameters();
        param.Add("p_Id", id);
        using var conn = context.GetConnection();
        var res = await conn.QueryFirstOrDefaultAsync(storedProcedure, param, commandType: CommandType.StoredProcedure);
        log.LogDebug($"Response: {res}");
        return res;
    }

    public async Task AddAsync(BaseModel entity)
    {
        log.LogInformation("[BaseDL] Add entity: {model}", entity);
        var storedProcedure = string.Format(ProcedureName.Insert, entity.GetType().Name);
        var parameters = new DynamicParameters(entity);
        using var conn = context.GetConnection();
        await conn.ExecuteAsync(storedProcedure, param: parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task<int> UpdateAsync(BaseModel entity, Guid id)
    {
        log.LogInformation("[{class}] Update entity: {model}", entity.GetType().Name, entity);
        var storedProcedure = string.Format(ProcedureName.Update, entity.GetType());

        var param = new DynamicParameters(entity);
        param.Add($"p_{id}", id);

        using var conn = context.GetConnection();
        return await conn.ExecuteAsync(
            storedProcedure,
            param,
            commandType: CommandType.StoredProcedure);
    }

    public async Task DeleteAsync(BaseModel model, Guid id)
    {
        log.LogInformation("[{BaseBL}] Delete model {model}", GetType().Name, model);
        var storedProcedure = string.Format(ProcedureName.Delete, model.GetType().Name);

        using var conn = context.GetConnection();
        await conn.ExecuteAsync(
            storedProcedure,
            new { p_Id = id },
            commandType: CommandType.StoredProcedure
        );
    }

    /// <summary>
    /// Kiem tra xem nhan vien co bi trung hay khong
    /// </summary>
    /// <param name="propName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public async Task<bool> CheckDuplicate(string propName, object value)
    {
        log.LogInformation("[{class}] Check duplicated employees", this.GetType().Name);
        string storedProcedure = string.Format(ProcedureName.CheckDuplicate, typeof(Employee).Name);
        var param = new DynamicParameters();
        param.Add($"p_{propName}", value);
        using var conn = context.GetConnection();
        var res = await conn.QueryFirstOrDefaultAsync<int>(storedProcedure, param,
            commandType: CommandType.StoredProcedure);
        return res > 0;
    }

    public async Task<object> ExecuteCommandText(string commandText, DynamicParameters param)
    {
        using var conn = context.GetConnection();
        return await conn.ExecuteAsync(commandText, param, commandType: CommandType.StoredProcedure);
    }

    public async Task<bool> CheckModelExists(BaseModel model, String id)
    {
        log.LogInformation("Check employee exists");
        // const string checkExistenceQuery = """
        //                                    SELECT 1 FROM Employees
        //                                    WHERE EmployeeCode = @EmployeeCode;
        //                                    """;
        // var result = await conn.ExecuteScalarAsync<int?>(checkExistenceQuery, new { EmployeeCode = code });
        // return result.HasValue;
        log.LogInformation("[{class}] Check model exists", model.GetType().Name);
        var storedProcedure = string.Format(ProcedureName.IsExist, model.GetType());
        using var conn = context.GetConnection();
        var result = await conn.QueryFirstOrDefaultAsync(
            storedProcedure,
            new { p_id = id },
            commandType: CommandType.StoredProcedure);

        return result;
    }
}