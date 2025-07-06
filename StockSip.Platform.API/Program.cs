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
using StockSip.Platform.API.Authorization.Application.Internal.ACL;
using StockSip.Platform.API.Authorization.Application.Internal.CommandServices;
using StockSip.Platform.API.Authorization.Application.Internal.OutboundServices;
using StockSip.Platform.API.Authorization.Application.Internal.QueryServices;
using StockSip.Platform.API.Authorization.Domain.Repositories;
using StockSip.Platform.API.Authorization.Domain.Services;
using StockSip.Platform.API.Authorization.Infrastructure.Hashing.BCrypt.Services;
using StockSip.Platform.API.Authorization.Infrastructure.Persistence.EFC.Repositories;
using StockSip.Platform.API.Authorization.Infrastructure.Pipeline.Middleware.Extensions;
using StockSip.Platform.API.Authorization.Infrastructure.Tokens.JWT.Configuration;
using StockSip.Platform.API.Authorization.Infrastructure.Tokens.JWT.Services;
using StockSip.Platform.API.Authorization.Interfaces.ACL;
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
using StockSip.Platform.API.PaymentAndSubscription.Application.Internal.OutboundServices.ACL;
using StockSip.Platform.API.PaymentAndSubscription.Application.Internal.QueryService;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Repositories;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Services;
using StockSip.Platform.API.PaymentAndSubscription.Infrastructure.Repositories;
using StockSip.Platform.API.PaymentAndSubscription.Interfaces.ACL;
using StockSip.Platform.API.Shared.Application.Internal.EventHandlers;
using StockSip.Platform.API.Shared.Domain.Repositories;
using StockSip.Platform.API.Shared.Infrastructure.Interfaces.ASP.Configuration;
using StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

var builder = WebApplication.CreateBuilder(args);

#region ────── MVC + CORS ──────────────────────────────────────────────

builder.Services.AddRouting(o => o.LowercaseUrls = true);
builder.Services.AddControllers(o => o.Conventions.Add(new KebabCaseRouteNamingConvention()));
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(o =>
{
    o.AddPolicy("AllowAllPolicy", p =>
        p.AllowAnyOrigin()
         .AllowAnyMethod()
         .AllowAnyHeader());
});

#endregion

#region ────── EF Core ─────────────────────────────────────────────────

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                      ?? throw new InvalidOperationException("Connection string not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
        options.UseMySQL(connectionString)
               .LogTo(Console.WriteLine, LogLevel.Information)
               .EnableSensitiveDataLogging()
               .EnableDetailedErrors();
    else
        options.UseMySQL(connectionString)
               .LogTo(Console.WriteLine, LogLevel.Error);
});

#endregion

#region ────── Swagger ────────────────────────────────────────────────

builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", new OpenApiInfo
    {
        Title       = "StockSip.Platform.API",
        Version     = "v1",
        Description = "StockSip Platform API",
        TermsOfService = new Uri("https://stocksip.com/tos"),
        Contact = new OpenApiContact { Name = "StockSip", Email = "contact@stocksip.com" },
        License = new OpenApiLicense
        {
            Name = "Apache 2.0",
            Url  = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
        }
    });

    // Annotations y nombres completos para evitar colisiones
    o.EnableAnnotations();
    o.CustomSchemaIds(t => t.FullName);

    // Bearer auth
    o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In           = ParameterLocation.Header,
        Name         = "Authorization",
        Type         = SecuritySchemeType.Http,
        Scheme       = "bearer",
        BearerFormat = "JWT",
        Description  = "Enter JWT token"
    });
    o.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Id = "Bearer", Type = ReferenceType.SecurityScheme }
            },
            Array.Empty<string>()
        }
    });
});

#endregion

#region ────── Dependency Injection ───────────────────────────────────

// Shared
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Alerts & Notifications
builder.Services.AddScoped<IAlertRepository, AlertRepository>();
builder.Services.AddScoped<IAlertCommandService, AlertCommandService>();
builder.Services.AddScoped<IAlertQueryService, AlertQueryService>();
builder.Services.AddScoped<IAlertsAndNotificationsContextFacade, AlertsAndNotificationsContextFacade>();

// Inventory Management
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

// Cloudinary
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();

builder.Services.AddScoped<IEventHandler<ProductProblemDetectedEvent>, ProductProblemDetectedEventHandler>();

// Payment & Subscription
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountQueryService, AccountQueryService>();
builder.Services.AddScoped<IAccountCommandService, AccountCommandService>();
builder.Services.AddScoped<IExternalAuthenticationService, ExternalAuthenticationService>();
builder.Services.AddScoped<IPaymentAndSubscriptionFacade, PaymentAndSubscriptionFacade>();
builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();

// Order Operation & Monitoring
builder.Services.AddScoped<ICatalogRepository, CatalogRepository>();
builder.Services.AddScoped<ICatalogCommandService, CatalogCommandService>();
builder.Services.AddScoped<ICatalogQueryService, CatalogQueryService>();
builder.Services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();
builder.Services.AddScoped<IPurchaseOrderCommandService, PurchaseOrderCommandService>();
builder.Services.AddScoped<IPurchaseOrderQueryService, PurchaseOrderQueryService>();

builder.Services.AddHttpClient<IAccountClient, AccountClient>(c =>
    c.BaseAddress = new Uri(builder.Configuration["AccountApi:BaseUrl"]!));

// Authentication / Authorization
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IAuthenticationContextFacade, AuthenticationContextFacade>();
builder.Services.AddHttpClient<IAccountClient, AccountClient>(client =>
{
    var url = builder.Configuration["AccountApi:BaseUrl"];
    if (string.IsNullOrWhiteSpace(url))
        throw new InvalidOperationException("Missing AccountApi:BaseUrl in configuration");

    client.BaseAddress = new Uri(url);
});
// Pipeline behaviors
builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(LoggingCommandBehavior<>));

#endregion

#region ────── Cortex Mediator ────────────────────────────────────────

builder.Services.AddCortexMediator(
    builder.Configuration,
    new[] { typeof(Program) },
    options => options.AddOpenCommandPipelineBehavior(typeof(LoggingCommandBehavior<>)));

#endregion

var app = builder.Build();

#region ────── Database auto‑create ───────────────────────────────────

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    ctx.Database.EnsureCreated();
}

#endregion

#region ────── HTTP Pipeline ──────────────────────────────────────────

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllPolicy");

app.UseRequestAuthorization(); // Middleware de autorización JWT custom
app.UseHttpsRedirection();
app.UseAuthorization();        // Policies / roles, etc.

app.MapControllers();
app.Run();

#endregion
