using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;

namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Entities;

/// <summary>
/// This class represents a Product Care Guide in the domain model.
/// </summary>
public class CareGuide
{
    /// <summary>
    /// The Product associated with the inventory, represented as a Product entity.
    /// </summary>
    public Product Product { get; internal set; }
    
    /// <summary>
    /// The unique identifier of the product in the inventory.
    /// </summary>
    public string ProductId { get; private set; }
    
    /// <summary>
    /// The title of the document.
    /// </summary>
    public string Title { get; private set; }

    /// <summary>
    /// The summary of all the recommendations.
    /// </summary>
    public string Summary { get; private set; }
    
    /// <summary>
    /// The recommended minimum temperature for a product.
    /// </summary>
    public double RecommendedMinTemperature { get; private set; }

    /// <summary>
    /// The recommended maximum temperature for a product.
    /// </summary>
    public double RecommendedMaxTemperature { get; private set; }
    
    /// <summary>
    /// The recommended place storage for the product.
    /// </summary>
    public string RecommendedPlaceStorage { get; private set; }
    
    /// <summary>
    /// A general recommendation about the product.
    /// </summary>
    public string GeneralRecommendation { get; private set; }

    /// <summary>
    /// Default constructor for the care guide entity.
    /// </summary>
    /// <param name="productId">
    /// The identifier of the product associated with this care guide.
    /// </param>
    /// <param name="title">
    /// The title of this care guide.
    /// </param>
    /// <param name="summary">
    /// The summary of all the recommendations.
    /// </param>
    /// <param name="minTemp">
    /// The recommended minimum temperature for a product.
    /// </param>
    /// <param name="maxTemp">
    /// The recommended maximum temperature for a product.
    /// </param>
    /// <param name="placeStorage">
    /// The recommended place storage for the product.
    /// </param>
    /// <param name="recommendation">
    /// A general recommendation about the product.
    /// </param>
    public CareGuide(
        string productId, 
        string title, 
        string summary, 
        double minTemp, 
        double maxTemp,
        string placeStorage, 
        string recommendation)
    {
        ProductId = productId;
        Title = title;
        Summary = summary;
        RecommendedMinTemperature = minTemp;
        RecommendedMaxTemperature = maxTemp;
        RecommendedPlaceStorage = placeStorage;
        GeneralRecommendation = recommendation;
    }

    /// <summary>
    /// This method is used to update the recommendations of the care guide.
    /// </summary>
    /// <param name="newTitle"></param>
    /// <param name="newSummary"></param>
    /// <param name="newMinTemp"></param>
    /// <param name="newMaxTemp"></param>
    /// <param name="newPlaceStorage"></param>
    /// <param name="newRecommendation"></param>
    public void UpdateRecommendations(
        string newTitle, 
        string newSummary, 
        double newMinTemp, 
        double newMaxTemp,
        string newPlaceStorage, 
        string newRecommendation)
    {
        Title = newTitle;
        Summary = newSummary;
        RecommendedMinTemperature = newMinTemp;
        RecommendedMaxTemperature = newMaxTemp;
        RecommendedPlaceStorage = newPlaceStorage;
        GeneralRecommendation = newRecommendation;
    }
}