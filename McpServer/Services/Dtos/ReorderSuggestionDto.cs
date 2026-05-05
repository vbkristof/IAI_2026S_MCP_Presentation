namespace McpServer.Services.Dtos;

public record ReorderSuggestionDto(
    int Id,
    string Name,
    string Sku,
    int CurrentStock,
    int MinimumStock,
    int TargetStock,
    string SupplierName,
    int SuggestedReorderQuantity
);
