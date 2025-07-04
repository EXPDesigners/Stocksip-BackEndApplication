using System.Xml.Schema;

namespace StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Commands;

public record CreateCatalogCommand(string Name, 
    string DateCreatedAt, 
    bool IsPublished, 
    string AccountId);