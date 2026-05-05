using McpServer.Models;
using Microsoft.EntityFrameworkCore;

namespace McpServer.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();
}