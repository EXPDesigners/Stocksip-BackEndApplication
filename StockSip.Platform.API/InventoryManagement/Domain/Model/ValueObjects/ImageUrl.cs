namespace StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

/// This value object represents the image URL.
/// <summary>
/// This record defines the image URL.
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
    public Uri? ImageUri { get; }

    /// <summary>
    /// The constructor initializes a new instance of the ImageUrl class with a default image URL.
    /// </summary>
    /// <param name="imageUri">The image url</param>
    public ImageUrl(string imageUri) : this()
    {
        ImageUri = string.IsNullOrWhiteSpace(imageUri) ? DefaultImageUrl : CreateValidateUrl(imageUri);
    }
    
    /// <summary>
    /// Validates and creates a URI from the provided image URL string.
    /// </summary>
    /// <param name="imageUri">The image Url</param>
    /// <returns>The image uri result</returns>
    /// <exception cref="ArgumentException">Validates if the image url be in the https protocol</exception>
    private static Uri CreateValidateUrl(string imageUri)
    {
        if (!Uri.TryCreate(imageUri, UriKind.Absolute, out var uriResult))
        {
            throw new ArgumentException("Image URL must be a valid absolute HTTPS URL.", nameof(imageUri));
        }

        if (!uriResult.Host.Equals("res.cloudinary.com", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("Image URL must be from Cloudinary CDN.");
        }
        
        return uriResult; 
    }
    
    public override string ToString()
    {
        return ImageUri?.AbsoluteUri ?? string.Empty;
    }
}