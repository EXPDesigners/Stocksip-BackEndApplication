namespace StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

/// <summary>
/// Represents an image URL for a product or a warehouse.
/// </summary>
public record ImageUrl()
{
    /// <summary>
    /// The URL of the image.
    /// </summary>
    private string Url { get; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ImageUrl"/> class with a specified image URL.
    /// </summary>
    /// <param name="imageUrl">
    /// The URL of the image.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when the provided image URL does not start with "https://res.cloudinary.com/".
    /// </exception>
    public ImageUrl(string imageUrl) : this()
    {
        if (!IsUrlValid(imageUrl))
        {
            throw new ArgumentException("Image URL must start with 'https://res.cloudinary.com/'.");
        }

        Url = imageUrl;
    }
    
    /// <summary>
    /// Method to validate if the provided image URL is valid.
    /// </summary>
    /// <param name="imageUrl">
    /// The URL of the image to validate.
    /// </param>
    /// <returns>
    /// Returns true if the URL starts with "https://res.cloudinary.com/", otherwise false.
    /// </returns>
    private static bool IsUrlValid(string imageUrl)
    {
        return imageUrl.StartsWith("https://res.cloudinary.com/");
    }

    /// <summary>
    /// Method to update the image URL.
    /// </summary>
    /// <param name="newImageUrl">
    /// The new image URL to update to.
    /// </param>
    /// <returns>
    /// Returns a new instance of <see cref="ImageUrl"/> with the updated URL.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the provided new image URL does not start with "https://res.cloudinary.com/".
    /// </exception>
    public ImageUrl UpdateImageUrl(string newImageUrl)
    {
        if (IsUrlValid(newImageUrl))
        {
            throw new ArgumentException("Updated Image URL must start with 'https://res.cloudinary.com/'.");
        }
        
        return new ImageUrl(newImageUrl);
    }
}