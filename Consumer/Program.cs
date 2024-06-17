using Carrefas.Core.Interfaces;
using Carrefas.Core.Notifications;
using Carrefas.FinancialFlow.Application.Interfaces;
using Carrefas.FinancialFlow.Application.Services;
using Carrefas.FinancialFlow.Data.Contexts;
using Carrefas.FinancialFlow.Data.Repositories;
using Carrefas.FinancialFlow.Domain.Interfaces;
using Carrefas.FinancialFlow.Domain.Services;
using Consumer;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddTransient<IFinancialFlowService, FinancialFlowService>();
builder.Services.AddTransient<IFinancialFlowAppService, FinancialFlowAppService>();
builder.Services.AddTransient<IFinancialFlowRepository, FinancialFlowRepository>();
builder.Services.AddTransient<CarrefasContext>();
builder.Services.AddTransient<INotifier, Notifier>();

var host = builder.Build();
host.Run();
