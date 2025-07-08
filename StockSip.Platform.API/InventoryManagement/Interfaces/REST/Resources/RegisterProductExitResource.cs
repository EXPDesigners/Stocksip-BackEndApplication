namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

/// <summary>
/// This record defines the RegisterProductExit resource.
/// </summary>
public record RegisterProductExitResource(DateOnly ExpirationDate, int QuantityExited, string ExitReason);