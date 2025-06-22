using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;
using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;

/// <summary>
/// This class represents a Product Care Guide in the domain model.
/// </summary>
public class CareGuide
{
    /// <summary>
    /// Unique identifier for the care guide.
    /// </summary>
    public string Id { get; } = Guid.NewGuid().ToString();
    
    /// <summary>
    /// The unique identifier of the account that owns this care guide.
    /// </summary>
    public AccountId AccountId { get; } 
    
    /// <summary>
    /// The Product associated with the inventory, represented as a Product entity.
    /// </summary>
    public Product? Product { get; internal set; }
    
    /// <summary>
    /// The unique identifier of the product in the inventory.
    /// </summary>
    public string? ProductId { get; private set; }
    
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
    /// Default constructor for the care guide entity assigning the product id.
    /// </summary>
    /// <param name="accountId">
    /// The identifier of the account owner of the care guide.
    /// </param> 
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
        AccountId accountId,
        string productId, 
        string title, 
        string summary, 
        double minTemp, 
        double maxTemp,
        string placeStorage, 
        string recommendation)
    {
        AccountId = accountId;
        ProductId = productId;
        Title = title;
        Summary = summary;
        RecommendedMinTemperature = minTemp;
        RecommendedMaxTemperature = maxTemp;
        RecommendedPlaceStorage = placeStorage;
        GeneralRecommendation = recommendation;
    }
    
    /// <summary>
    /// Default constructor for the care guide entity without assigning the product id.
    /// </summary>
    /// <param name="accountId"></param>
    /// <param name="title"></param>
    /// <param name="summary"></param>
    /// <param name="minTemp"></param>
    /// <param name="maxTemp"></param>
    /// <param name="placeStorage"></param>
    /// <param name="recommendation"></param>
    public CareGuide(
        AccountId accountId,
        string title, 
        string summary, 
        double minTemp, 
        double maxTemp,
        string placeStorage, 
        string recommendation)
    {
        AccountId = accountId;
        Title = title;
        Summary = summary;
        RecommendedMinTemperature = minTemp;
        RecommendedMaxTemperature = maxTemp;
        RecommendedPlaceStorage = placeStorage;
        GeneralRecommendation = recommendation;
    }

    /// <summary>
    /// Default constructor for handling commands
    /// </summary>
    /// <param name="command">
    /// The command containing the details for creating a new care guide.
    /// </param>
    public CareGuide(CreateCareGuideCommand command) : this(command.AccountId, command.ProductId, command.Title, command.Summary, command.MinTemp, command.MaxTemp, command.PlaceStorage, command.Recommendation)
    {
    }

    /// <summary>
    /// This method is used to update the recommendations of the care guide.
    /// </summary>
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
    
    /// <summary>
    /// This method is used to unassign the care guide of the current product.
    /// </summary>
    public void UnassignCareGuide()
    {
        ProductId = "";
        Product = null;
    }
    
    /// <summary>
    /// Method for assigning this care guide to another product.
    /// </summary>
    /// <param name="newProductId"></param>
    /// <exception cref="ArgumentException"></exception>
    public void AssignCareGuideToAnotherProduct(string newProductId)
    {
        if (newProductId == ProductId)
        {
            throw new ArgumentException("Cannot assign a care guide to the same product.");
        }
        else
        {
            ProductId = newProductId;
        }
    }
}