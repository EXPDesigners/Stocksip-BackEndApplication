namespace StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

/// This value object represents the image URL of a Product or Warehouse.
/// <summary>
/// This record defines the image URL for a product or warehouse.
/// </summary>
public record ImageUrl()
{
    /// <summary>
    /// The default image URL used when no specific image URL is provided.
    /// </summary>
    private static readonly Uri DefaultImageUrl = new ("https://res.cloudinary.com/deuy1pr9e/image/upload/v1747454213/g24tiltaf9nughb8km93.avif");
    
    /// <summary>
    /// The image URL for the product or warehouse.
    /// </summary>
    private Uri? ImageUri { get; }

    /// <summary>
    /// The default constructor for the ImageUrl record.
    /// </summary>
    /// <param name="imageUri">The image URL for products or warehouses</param>
    /// <exception cref="ArgumentException">The image URL cannot be null or empty and need to start with https://res.cloudinary.com/</exception>
    public ImageUrl(string imageUri) : this()
    {
        if (!string.IsNullOrWhiteSpace(imageUri))
        {
            throw new ArgumentException("Image URL cannot be null or empty.", nameof(imageUri));
        }
        
        if (!Uri.TryCreate(imageUri, UriKind.Absolute, out var uriResult))
        {
            throw new ArgumentException("Image URL must be a valid absolute HTTPS URL.", nameof(imageUri));
        }

        if (!uriResult.Host.Equals("res.cloudinary.com", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("Image URL must be from Cloudinary CDN.");
        }
        
        ImageUri = uriResult;
    }

    /// <summary>
    /// Sets the default image URL for products or warehouses.
    /// </summary>
    /// <returns>The default image URL</returns>
    public static ImageUrl DefaultImage() => new(DefaultImageUrl.ToString());
}