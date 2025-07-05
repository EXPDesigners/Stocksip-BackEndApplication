using Cortex.Mediator.Behaviors;
using Cortex.Mediator.Commands;
using Cortex.Mediator.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using StockSip.Platform.API.AlertsAndNotifications.Application.ACL;
using StockSip.Platform.API.AlertsAndNotifications.Application.Internal.CommandServices;
using StockSip.Platform.API.AlertsAndNotifications.Application.Internal.QueryServices;
using StockSip.Platform.API.AlertsAndNotifications.Domain.Repositories;
using StockSip.Platform.API.AlertsAndNotifications.Domain.Services;
using StockSip.Platform.API.AlertsAndNotifications.Infrastructure.Persistence.EFC.Repositories;
using StockSip.Platform.API.AlertsAndNotifications.Interfaces.ACL;
using StockSip.Platform.API.InventoryManagement.Application.ACL;
using StockSip.Platform.API.InventoryManagement.Application.Internal.CommandService;
using StockSip.Platform.API.InventoryManagement.Application.Internal.EventHandlers;
using StockSip.Platform.API.InventoryManagement.Application.Internal.OutboundServices.Cloudinary;
using StockSip.Platform.API.InventoryManagement.Application.Internal.QueryService;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Events;
using StockSip.Platform.API.InventoryManagement.Domain.Repositories;
using StockSip.Platform.API.InventoryManagement.Domain.Services;
using StockSip.Platform.API.InventoryManagement.Infrastructure.FileStorage.Cloudinary.Configuration;
using StockSip.Platform.API.InventoryManagement.Infrastructure.FileStorage.Cloudinary.Services;
using StockSip.Platform.API.InventoryManagement.Infrastructure.Persistence.EFC.Repositories;
using StockSip.Platform.API.OrderOperationAndMonitoring.Application.Internal.CommandService;
using StockSip.Platform.API.OrderOperationAndMonitoring.Application.Internal.QueryService;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.External;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Repositories;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Services;
using StockSip.Platform.API.OrderOperationAndMonitoring.Infrastructure.External;
using StockSip.Platform.API.OrderOperationAndMonitoring.Infrastructure.Persistence.EFC.Repositories;
using StockSip.Platform.API.PaymentAndSubscription.Application.Internal.CommandService;
using StockSip.Platform.API.PaymentAndSubscription.Application.Internal.QueryService;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Repositories;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Services;
using StockSip.Platform.API.PaymentAndSubscription.Infrastructure.Repositories;
using StockSip.Platform.API.Shared.Application.Internal.EventHandlers;
using StockSip.Platform.API.Shared.Domain.Repositories;
using StockSip.Platform.API.Shared.Infrastructure.Interfaces.ASP.Configuration;
using StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add ASP.NET Core MVC with Kebab Case Route Naming Convention
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));
builder.Services.AddEndpointsApiExplorer();

// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Add Configuration for Entity Framework Core
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (connectionString == null) throw new InvalidOperationException("Connection string not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    else if (builder.Environment.IsProduction())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Error);
});

// Add Swagger/OpenAPI support
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "StockSip.Platform.API",
        Version = "v1",
        Description = "StockSip Platform API for Inventory Management",
        TermsOfService = new Uri("https://stocksip.com/tos"),
        Contact = new OpenApiContact
        {
            Name = "StockSip",
            Email = "contact@stocksip.com"
        },
        License = new OpenApiLicense
        {
            Name = "Apache 2.0",
            Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
        },
    });
    options.EnableAnnotations();
    options.CustomSchemaIds(type => type.FullName);
});

// Dependency Injection

// Shared Bounded Context
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Alerts And Notifications - Bounded Context
builder.Services.AddScoped<IAlertRepository, AlertRepository>();
builder.Services.AddScoped<IAlertCommandService, AlertCommandService>();
builder.Services.AddScoped<IAlertQueryService, AlertQueryService>();
builder.Services.AddScoped<IAlertsAndNotificationsContextFacade, AlertsAndNotificationsContextFacade>();

// Inventory Management - Bounded Context
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();
builder.Services.AddScoped<IWarehouseCommandService, WarehouseCommandService>();
builder.Services.AddScoped<IWarehouseQueryService, WarehouseQueryService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductCommandService, ProductCommandService>();
builder.Services.AddScoped<IProductQueryService, ProductQueryService>();
builder.Services.AddScoped<ICareGuideRepository, CareGuideRepository>();
builder.Services.AddScoped<ICareGuideCommandService, CareGuideCommandService>();
builder.Services.AddScoped<ICareGuideQueryService, CareGuideQueryService>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IInventoryCommandService, InventoryCommandService>();
builder.Services.AddScoped<IInventoryQueryService, InventoryQueryService>();
builder.Services.AddScoped<ExternalAlertsAndNotificationsService>();

// Cloudinary Configuration
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();

builder.Services.AddScoped<IEventHandler<ProductProblemDetectedEvent>, ProductProblemDetectedEventHandler>();

// Payment and Subscription - Bounded Context
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountQueryService, AccountQueryService>();
builder.Services.AddScoped<IAccountCommandService, AccountCommandService>();
builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();

// Order Operation and Monitoring - Bounded Context
builder.Services.AddScoped<ICatalogRepository, CatalogRepository>();
builder.Services.AddScoped<ICatalogCommandService, CatalogCommandService>();
builder.Services.AddScoped<ICatalogQueryService, CatalogQueryService>();
builder.Services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();
builder.Services.AddScoped<IPurchaseOrderCommandService, PurchaseOrderCommandService>();
builder.Services.AddScoped<IPurchaseOrderQueryService, PurchaseOrderQueryService>();


builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(LoggingCommandBehavior<>));

builder.Services.AddHttpClient<IAccountClient, AccountClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["AccountApi:BaseUrl"]);
});

// Add Mediator for CQRS
builder.Services.AddCortexMediator(
    configuration: builder.Configuration,
    handlerAssemblyMarkerTypes: new[] { typeof(Program) }, configure: options =>
    {
        options.AddOpenCommandPipelineBehavior(typeof(LoggingCommandBehavior<>));
        //options.AddDefaultBehaviors();
    });

var app = builder.Build();

// Verify if the database exists and create it if it doesn't
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();

    context.Database.EnsureCreated();
}

// Use Swagger for API documentation if in development mode
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Apply CORS Policy
app.UseCors("AllowAllPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();