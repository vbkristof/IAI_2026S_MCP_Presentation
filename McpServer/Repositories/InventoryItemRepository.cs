using McpServer.Data;
using McpServer.Models;
using Microsoft.EntityFrameworkCore;

namespace McpServer.Repositories;

public class InventoryItemRepository(DataContext dbContext)
    : BaseRepository<InventoryItem, int>(dbContext)
{
    private readonly DataContext _dbContext = dbContext;

    public new async Task<List<InventoryItem>> GetAllAsync() =>
        await _dbContext.InventoryItems
            .AsNoTracking()
            .OrderBy(i => i.Name)
            .ToListAsync();

    public async Task<List<InventoryItem>> GetLowStockItemsAsync() =>
        await _dbContext.InventoryItems
            .AsNoTracking()
            .Where(i => i.CurrentStock < i.MinimumStock)
            .OrderBy(i => i.Name)
            .ToListAsync();

    public async Task<List<InventoryItem>> GetItemsNeedingReorderAsync() =>
        await _dbContext.InventoryItems
            .AsNoTracking()
            .Where(i => i.TargetStock - i.CurrentStock > 0)
            .OrderBy(i => i.Name)
            .ToListAsync();
}
