using DeliveryService.API.Handlers;
using DeliveryService.Application.Common.Behaviors;
using DeliveryService.Application.Interfaces;
using DeliveryService.Persistence;
using DeliveryService.Persistence.Settings;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using System.ComponentModel;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddSerilog(config =>
{
    config.ReadFrom.Configuration(builder.Configuration);
    config.Enrich.FromLogContext();
});

builder.Services.Configure<DataBaseSet>(builder.Configuration.GetSection(DataBaseSet.Configuration));

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(builder.Configuration.GetSection(DataBaseSet.Configuration).Get<DataBaseSet>().ConnectionString));
builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

builder.Services.AddExceptionHandler<HandlerException>();
builder.Services.AddProblemDetails();

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    string basePath = AppContext.BaseDirectory;

    string xmlPath = Path.Combine(basePath, "Documentation.xml");
    options.IncludeXmlComments(xmlPath);
});

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();
app.MapControllers();

using (IServiceScope scope = app.Services.CreateScope())
{
    ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    await Preparation.Initialize(context);
}

app.Run();
