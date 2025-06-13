namespace StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

/// <summary>
/// This enum defines the possible states of liquor.
/// The possible states are:
/// - WITH_STOCK: When the product has enough stocks to do the normal operations with it.
/// - OUT_OF_STOCK: When the product does not have a stock or is equal to 0.
/// </summary>
public enum EProductState
{
    OutOfStock,
    InStock
}