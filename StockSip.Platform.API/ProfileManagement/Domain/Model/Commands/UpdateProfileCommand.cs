namespace StockSip.Platform.API.ProfileManagement.Domain.Model.Commands;

public record UpdateProfileCommand(
    string Name,
    string Email,
    string PhoneNumber,
    string Role,
    string BusinessName,
    string BusinessAddress
    ) : CreateProfileCommand(
        Name,
        Email,
        PhoneNumber,
        Role,
        BusinessName,
        BusinessAddress
    );