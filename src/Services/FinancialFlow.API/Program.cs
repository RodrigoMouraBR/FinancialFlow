using FinancialFlow.API.Configuration;
using FinancialFlow.Application.AutoMapper;
using FinancialFlow.Core.EnvironmentVariable;
using FinancialFlow.Data.Contexts;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using System.Runtime;

var builder = WebApplication.CreateBuilder(args);

#region appsettings.json
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();
#endregion

#region Connections DB
builder.Services.AddConnectionUseNpgsql(builder.Configuration);
#endregion

#region Configuration

builder.Services.AddApiConfig();
builder.Services.AddSwaggerConfig();
builder.Services.Configure<EnvironmentSetting>(builder.Configuration.GetSection("EnvironmentSetting"));

#endregion

#region IoC

builder.Services.ResolveDependencies();

#endregion

#region AutoMapper

builder.Services.AddAutoMapper(typeof(FinancialFlowMappingConfig));


#endregion

var app = builder.Build();
var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure
app.UseApiConfig(app.Environment);
app.UseSwaggerConfig(apiVersionDescriptionProvider);

//DbMigrationHelpers.EnsureSeedData(app).Wait();

app.Run();
