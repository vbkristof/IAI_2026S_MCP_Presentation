namespace McpServer.Models;

public class InventoryItem() : BaseEntity<int>(0)
{
    public string Name { get; set; } = string.Empty;
    public string Sku { get; set; } = string.Empty;
    public int CurrentStock { get; set; }
    public int MinimumStock { get; set; }
    public int TargetStock { get; set; }
    public string SupplierName { get; set; } = string.Empty;
}
