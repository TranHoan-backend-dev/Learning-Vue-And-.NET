using MISA.BL.Base;
using MISA.BL.Service;
using MISA.DL.Base;
using MISA.DL.Repository;
using MISA.DL.Context;
using MISA.API.Exception;
using Dapper;
using MISA.Common.Extension;

var builder = WebApplication.CreateBuilder(args);

// Register Dapper TypeHandlers
SqlMapper.AddTypeHandler(new GuidTypeHandler());

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped(typeof(IBaseBl<>), typeof(BaseBl<>));
builder.Services.AddScoped(typeof(IBaseDl<>), typeof(BaseDl<>));
builder.Services.AddScoped<DbContext>();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();


// app.UseHttpsRedirection();


app.Run();