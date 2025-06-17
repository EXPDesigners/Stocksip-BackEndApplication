namespace StockSip.Platform.API.ProfileManagement.Domain.Model.Commands;

public record CreateProfileCommand(
    string Name,
    string Email,
    string PhoneNumber,
    string Role,
    string BusinessName,
    string BusinessAddress);