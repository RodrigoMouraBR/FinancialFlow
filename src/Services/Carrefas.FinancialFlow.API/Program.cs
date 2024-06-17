using Carrefas.FinancialFlow.API.Configuration;
using Carrefas.FinancialFlow.Application.AutoMapper;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Carrefas.FinancialFlow.Data.Contexts;
using RabbitMQ.Client;
using Carrefas.FinancialFlow.Application.Interfaces;
using Carrefas.FinancialFlow.Application.Services;
using Carrefas.FinancialFlow.Domain.Queue;
using Microsoft.Extensions.Configuration;

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

#endregion

#region IoC

builder.Services.ResolveDependencies();

builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQ"));
builder.Services.AddSingleton<IRabbitMqService, RabbitMqService>();
builder.Services.AddHostedService<RabbitMQHostedService>();

#endregion

#region AutoMapper

builder.Services.AddAutoMapper(typeof(FinancialFlowMappingConfig));


#endregion




var app = builder.Build();

var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure
app.UseApiConfig(app.Environment);
app.UseSwaggerConfig(apiVersionDescriptionProvider);
app.Run();