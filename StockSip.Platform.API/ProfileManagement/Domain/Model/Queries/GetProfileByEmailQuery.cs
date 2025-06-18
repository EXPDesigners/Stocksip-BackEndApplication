using StockSip.Platform.API.ProfileManagement.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.ProfileManagement.Domain.Model.Queries;

public record GetProfileByEmailQuery(UserEmail Email);