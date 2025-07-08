namespace StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

/// <summary>
/// Represents a unique identifier for a provider who is a user.
/// </summary>
/// <param name="Id">
/// The unique identifier for the provider.
/// </param>
public record ProviderId(string Id);