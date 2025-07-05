// Domain/Model/Commands/CreateCatalogCommand.cs
namespace StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Commands;

/// <summary>
/// Command to create a new catalog.
/// </summary>
public record CreateCatalogCommand
{
    public string AccountId { get; }
    public string Name      { get; }

    public CreateCatalogCommand(string accountId, string name)
    {
        if (string.IsNullOrWhiteSpace(accountId))
            throw new ArgumentException("accountId is required", nameof(accountId));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("name is required", nameof(name));

        AccountId = accountId;
        Name      = name;
    }
}