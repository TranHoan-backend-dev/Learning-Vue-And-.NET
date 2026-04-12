using MISA.BL.Base;
using MISA.BL.Service;
using MISA.DL.Base;
using MISA.DL.Repository;
using MISA.DL.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped(typeof(IBaseBl<>), typeof(BaseBl<>));
builder.Services.AddScoped(typeof(IBaseDl<>), typeof(BaseDl<>));
builder.Services.AddScoped<DbContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();


// app.UseHttpsRedirection();


app.Run();