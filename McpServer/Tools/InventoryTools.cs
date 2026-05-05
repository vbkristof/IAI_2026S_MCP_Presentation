using System.ComponentModel;
using McpServer.Services;
using McpServer.Services.Dtos;
using ModelContextProtocol.Server;

namespace McpServer.Tools;

[McpServerToolType]
public class InventoryTools(InventoryService inventoryService)
{
    [McpServerTool(Name = "list_inventory_items")]
    [Description("Lists all inventory items from the ERP inventory table.")]
    public Task<List<InventoryItemDto>> ListInventoryItemsAsync() =>
        inventoryService.ListInventoryItemsAsync();

    [McpServerTool(Name = "get_low_stock_items")]
    [Description("Lists inventory items where current stock is below minimum stock.")]
    public Task<List<InventoryItemDto>> GetLowStockItemsAsync() =>
        inventoryService.GetLowStockItemsAsync();

    [McpServerTool(Name = "create_reorder_suggestion")]
    [Description("Creates reorder suggestions using target stock minus current stock.")]
    public Task<List<ReorderSuggestionDto>> CreateReorderSuggestionsAsync() =>
        inventoryService.CreateReorderSuggestionsAsync();
}
