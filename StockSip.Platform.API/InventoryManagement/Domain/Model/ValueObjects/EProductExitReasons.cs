namespace StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

/// <summary>
/// This enum defines the possible reasons about the exit of a product. This is used to track the reason of the exit of a product.
/// The possible states are:
/// - SOLD: When the product is sold.
/// - DONATED: When the product is donated.
/// - EXPIRED: When the product is lost because of the expiration date has passed.
/// - SPOILED: When the product is lost because of the conditions it was stored.
/// - DAMAGED: When the product packaging is damaged and can no longer be sold.
/// - BROKE: When the product fell and broke.
/// - CONSUMED: When a product is consumed internally by the owner of the liquor store.
/// </summary>
public enum EProductExitReasons
{
    Sold,
    Donated,
    Expired,
    Spoiled,
    Damaged,
    Broke,
    Consumed
}