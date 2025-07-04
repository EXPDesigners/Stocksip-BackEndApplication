using StockSip.Platform.API.Authorization.Application.Internal.OutboundServices;
using StockSip.Platform.API.Authorization.Domain.Model.Aggregate;
using StockSip.Platform.API.Authorization.Domain.Model.Commands;
using StockSip.Platform.API.Authorization.Domain.Repositories;
using StockSip.Platform.API.Authorization.Domain.Services;
using StockSip.Platform.API.PaymentAndSubscription.Interfaces.ACL;
using StockSip.Platform.API.Shared.Domain.Repositories;

namespace StockSip.Platform.API.Authorization.Application.Internal.CommandServices;

/// <summary>
/// Defines the command service for user operations such as sign-in and sign-up.
/// </summary>
public class UserCommandService(IUserRepository userRepository, 
                                ITokenService tokenService,
                                IHashingService hashingService,
                                IPaymentAndSubscriptionFacade paymentAndSubscriptionFacade,
                                IUnitOfWork unitOfWork) : IUserCommandService
{
    /// <summary>
    /// This method handles the sign-in command by verifying the user's credentials and generating a token if successful.
    /// </summary>
    /// <param name="command">The sign-in command containing the username and password.</param>
    /// <returns>A tuple containing the user and the generated token.</returns>
    /// <exception cref="Exception">A general exception is thrown if the username or password is invalid.</exception>
    public async Task<(User user, string token, string? accountId)> Handle(SignInCommand command)
    {
        var user = await userRepository.FindByUsernameAsync(command.Username);

        if (user == null || !hashingService.VerifyPassword(command.Password, user.PasswordHash))
            throw new Exception("Invalid username or password");

        var token = tokenService.GenerateToken(user);
        var accountId = await paymentAndSubscriptionFacade.GetAccountIdByUserIdAsync(user.UserId);

        return (user, token, accountId);
    }

    /// <summary>
    /// This method handles the sign-up command by creating a new user with the provided username and password.
    /// </summary>
    /// <param name="command">The sign-up command containing the username and password.</param>
    /// <exception cref="Exception">A general exception is thrown if the username is already taken or if an error occurs during user creation.</exception>
    public async Task Handle(SignUpCommand command)
    {
        if (userRepository.ExistsByUsername(command.Username))
            throw new Exception($"Username {command.Username} is already taken");

        var hashedPassword = hashingService.HashPassword(command.Password);
        var user = new User(command.Username, hashedPassword);
        try
        {
            await userRepository.AddAsync(user);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"An error occurred while creating user: {e.Message}");
        }
    }
}