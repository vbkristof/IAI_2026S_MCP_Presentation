namespace McpServer.Services.Dtos;

public record InventoryItemDto(
    int Id,
    string Name,
    string Sku,
    int CurrentStock,
    int MinimumStock,
    int TargetStock,
    string SupplierName
);
