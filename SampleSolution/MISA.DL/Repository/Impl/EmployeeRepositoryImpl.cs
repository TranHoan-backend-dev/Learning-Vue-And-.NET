// using System.Data;
// using Dapper;
// using DL.Context;
// using DL.Exception;
// using DL.Repository.Interface;
// using Microsoft.Extensions.Logging;
// using Model.Base;
// using Model.Model;
//
// namespace DL.Repository.Impl;
//
// public class EmployeeRepositoryImpl(
//     DbContext context,
//     ILogger<EmployeeRepositoryImpl> log
// ) : IEmployeeRepository
// {
//     public async Task<IEnumerable<Employee>?> GetAllAsync()
//     {
//         log.LogInformation("Get all employees");
//         using var conn = context.GetConnection();
//         const string query = "SELECT * FROM Employees";
//         log.LogDebug("Query {@Query}", query);
//         return await conn.QueryAsync<Employee>(query);
//     }
//
//     public Task<IEnumerable<Employee>?> GetPagedAsync(int pageNumber, int pageSize)
//     {
//         throw new NotImplementedException();
//     }
//
//     public async Task<Employee?> GetByIdAsync(Guid id)
//     {
//         log.LogInformation("Get employee by id");
//         using var conn = context.GetConnection();
//         const string query = """
//                              SELECT * FROM Employees
//                              WHERE EmployeeId = @id;
//                              """;
//         var result = await conn.QueryFirstOrDefaultAsync<Employee>(query, new { id });
//         log.LogDebug("Result: {@Employee}", result);
//         return result;
//     }
//
//     public async Task AddAsync(Employee entity)
//     {
//         log.LogInformation("Add employee to database");
//         using var conn = context.GetConnection();
//
//         // Kiem tra xem ban ghi voi code nay da ton tai hay chua
//         // Neu ton tai roi thi nem exception
//         var result = CheckModelExists(conn, entity.EmployeeCode);
//         if (result.Result)
//         {
//             throw new AlreadyExistsException("Employee already exists");
//         }
//
//         const string insertQuery = """
//                                    INSERT INTO Employees (employee_code, fullname, gender, email, age)
//                                    VALUES (@EmployeeCode, @FullName, @Gender, @Email, @Age)
//                                    """;
//         await conn.ExecuteAsync(insertQuery, new
//         {
//             entity.EmployeeCode, entity.FullName, entity.Gender,
//             entity.Email, entity.Age
//         });
//     }
//
//     public async Task AddAsync1(BaseModel model)
//     {
//         log.LogInformation("Add employee to database");
//         string className = typeof(BaseModel).Name;
//         string storedProcedure = string.Format(ProcedureName.Insert, typeof(BaseModel).Name);
//         var param = new DynamicParameters();
//         var listProperties = typeof(BaseModel).GetProperties();
//         foreach (var property in listProperties)
//         {
//             param.Add($"p_{property.Name}", property.GetValue(model));
//         }
//
//         using var conn = context.GetConnection();
//         var res = await conn.ExecuteAsync(storedProcedure, param, commandType: CommandType.StoredProcedure);
//     }
//
//     public async Task UpdateAsync(Employee entity)
//     {
//         log.LogInformation("Update employee");
//         using var connection = context.GetConnection();
//         // Lay ban ghi cu
//         const string getQuery = "SELECT * FROM Employees WHERE employee_code = @EmployeeCode;";
//         var employee = await connection.QueryFirstOrDefaultAsync<Employee>(getQuery, new { entity.EmployeeCode });
//         if (employee is null)
//         {
//             throw new NotFoundException("Employee not found");
//         }
//
//         // tao param de cap nhat
//         var updates = new List<string>();
//         var parameters = new DynamicParameters();
//
//         parameters.Add("EmployeeCode", entity.EmployeeCode);
//
//         if (!entity.FullName.ToLower().Equals(employee.FullName.ToLower()))
//         {
//             updates.Add("fullname = @FullName");
//             parameters.Add("FullName", employee.FullName);
//         }
//
//         if (entity.Gender != employee.Gender)
//         {
//             updates.Add("gender = @Gender");
//             parameters.Add("Gender", entity.Gender);
//         }
//
//         if (entity.Email != employee.Email)
//         {
//             updates.Add("email = @Email");
//             parameters.Add("Email", entity.Email);
//         }
//
//         if (entity.Age != employee.Age)
//         {
//             updates.Add("age = @Age");
//             parameters.Add("Age", entity.Age);
//         }
//
//         if (updates.Count == 0)
//         {
//             log.LogInformation("No updates found");
//             return;
//         }
//
//         string query = $"""
//                         UPDATE Employees
//                         SET {string.Join(", ", updates)}
//                         WHERE employee_code = @EmployeeCode
//                         """;
//         await connection.ExecuteAsync(query, parameters);
//         log.LogDebug("Update success");
//     }
//
//     public async Task DeleteAsync(Guid id)
//     {
//         log.LogInformation("Delete employee");
//         using var conn = context.GetConnection();
//         {
//             const string getQuery = "SELECT * FROM Employees WHERE employee_id = @EmployeeId;";
//             var result = await conn.QueryFirstOrDefaultAsync<Employee>(getQuery, new { EmployeeId = id });
//             if (result == null)
//             {
//                 throw new NotFoundException("Employee not found");
//             }
//         }
//         const string deleteQuery = "DELETE FROM Employees WHERE employee_id = @EmployeeId;";
//         await conn.ExecuteAsync(deleteQuery, new { EmployeeId = id });
//         log.LogDebug("Delete employee success");
//     }
//
//     /// <summary>
//     /// Kiem tra xem nhan vien co bi trung hay khong
//     /// </summary>
//     /// <param name="propName"></param>
//     /// <param name="value"></param>
//     /// <returns></returns>
//     public async Task<bool> CheckDuplicate(string propName, object value)
//     {
//         log.LogInformation("Check duplicated employees");
//         string storedProcedure = string.Format(ProcedureName.CheckDuplicate, typeof(Employee).Name);
//         var param = new DynamicParameters();
//         param.Add($"p_{propName}", value);
//         using var conn = context.GetConnection();
//         var res = await conn.QueryFirstOrDefaultAsync<int>(storedProcedure, param,
//             commandType: CommandType.StoredProcedure);
//         return res > 0;
//     }
//
//     private async Task<bool> CheckModelExists(IDbConnection conn, String code)
//     {
//         const string checkExistenceQuery = """
//                                            SELECT 1 FROM Employees
//                                            WHERE EmployeeCode = @EmployeeCode;
//                                            """;
//         var result = await conn.ExecuteScalarAsync<int?>(checkExistenceQuery, new { EmployeeCode = code });
//         return result.HasValue;
//     }
// }