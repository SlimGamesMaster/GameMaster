using Microsoft.EntityFrameworkCore;
using GameMasterEnterprise.Data.Context;
using GameMasterEnterprise.API.Configuration;
using GameMasterEnterprise.APIConfiguration;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

builder.Services.AddDbContext<MeuDbContext>(options =>
{
    var connectionString = "server=localhost;port=3310;user=root;password=root;database=GameMasterEnterprise;";


    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
           .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});


builder.Services.ResolveDependencies();
builder.Services.AddIdentityConfig(builder.Configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddApiConfig();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseApiConfig(app.Environment);
app.UseHttpsRedirection();

app.UseAuthorization();

app.Run();
