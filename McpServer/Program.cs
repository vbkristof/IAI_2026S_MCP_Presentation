using McpServer.Data;
using McpServer.Repositories;
using McpServer.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseLazyLoadingProxies();
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Repositories
builder.Services.AddScoped<InventoryItemRepository>();

// Services
builder.Services.AddScoped<InventoryService>();

// MCP server
builder.Services
    .AddMcpServer()
    .WithHttpTransport(options => options.Stateless = true)
    .WithToolsFromAssembly();

var app = builder.Build();

await DatabaseInitializer.MigrateAndSeedAsync(app.Services);

app.MapMcp();

await app.RunAsync();