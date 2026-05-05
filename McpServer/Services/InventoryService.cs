using McpServer.Models;
using McpServer.Repositories;
using McpServer.Services.Dtos;

namespace McpServer.Services;

public class InventoryService(InventoryItemRepository inventoryItemRepository)
{
    public async Task<List<InventoryItemDto>> ListInventoryItemsAsync()
    {
        List<InventoryItem> items = await inventoryItemRepository.GetAllAsync();
        return items.Select(ToDto).ToList();
    }

    public async Task<List<InventoryItemDto>> GetLowStockItemsAsync()
    {
        List<InventoryItem> items = await inventoryItemRepository.GetLowStockItemsAsync();
        return items.Select(ToDto).ToList();
    }

    public async Task<List<ReorderSuggestionDto>> CreateReorderSuggestionsAsync()
    {
        List<InventoryItem> items = await inventoryItemRepository.GetItemsNeedingReorderAsync();
        return items.Select(i => new ReorderSuggestionDto(
            i.Id,
            i.Name,
            i.Sku,
            i.CurrentStock,
            i.MinimumStock,
            i.TargetStock,
            i.SupplierName,
            i.TargetStock - i.CurrentStock
        )).ToList();
    }

    private static InventoryItemDto ToDto(InventoryItem i) =>
        new(i.Id, i.Name, i.Sku, i.CurrentStock, i.MinimumStock, i.TargetStock, i.SupplierName);
}
