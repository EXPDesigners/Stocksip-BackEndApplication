using StockSip.Platform.API.Authorization.Application.Internal.OutboundServices.Email;
using StockSip.Platform.API.Authorization.Application.Internal.OutboundServices.Hashing;
using StockSip.Platform.API.Authorization.Application.Internal.OutboundServices.Token;
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
public class UserCommandService(
    IUserRepository                  userRepository,
    ITokenService                    tokenService,
    IHashingService                  hashingService,
    IPaymentAndSubscriptionFacade    paymentAndSubscriptionFacade,
    IUnitOfWork                      unitOfWork,
    IEmailService                    emailService) : IUserCommandService
{
    /// <summary>
    /// Handles the sign-in process by validating user credentials, generating a token,
    /// and retrieving account-related details such as account ID and role if applicable.
    /// </summary>
    /// <param name="command">The sign-in command containing user credentials (username and password).</param>
    /// <returns>A tuple containing the user object, a generated authentication token,
    /// the corresponding account ID (if available), and the account role (if applicable).</returns>
    /// <exception cref="Exception">Thrown if the username or password is invalid.</exception>
    public async Task<(User user, string token, string? accountId, string? accountRole)> Handle(SignInCommand command)
    {
        var user = await userRepository.FindByUsernameAsync(command.Username);

        if (user is null || !hashingService.VerifyPassword(command.Password, user.PasswordHash))
            throw new Exception("Invalid username or password");

        var token      = tokenService.GenerateToken(user);
        var accountId  = await paymentAndSubscriptionFacade.GetAccountIdByUserIdAsync(user.UserId);
        var accountRole = accountId is null
            ? null
            : await paymentAndSubscriptionFacade.GetAccountRoleByAccountIdAsync(accountId);

        return (user, token, accountId, accountRole);
    }
    

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

    public async Task Handle(SendRecoveryCodeCommand command)
    {
        var user = await userRepository.FindByUsernameAsync(command.Username)
                   ?? throw new ArgumentException("No user found with this email");

        var code = new Random().Next(100000, 999999).ToString();
        user.SetRecoveryCode(code, TimeSpan.FromMinutes(15));
        
        await emailService.SendPasswordRecoveryEmail(user.Username, code);
        await unitOfWork.CompleteAsync();
    }

    public async Task Handle(VerifyRecoveryCodeCommand command)
    {
        var user = await userRepository.FindByUsernameAsync(command.username)
                   ?? throw new ArgumentException("No user found with this email");
        
        if (!user.IsRecoveryCodeValid(command.RecoverCode))
            throw new Exception("Invalid recovery code");
        
        user.ClearRecoveryCode();
        await unitOfWork.CompleteAsync();
    }

    public async Task Handle(ResetPasswordCommand command)
    {
        var user = await userRepository.FindByUsernameAsync(command.Username)
                   ?? throw new ArgumentException("No user found with this email");
        
        var hashedPassword = hashingService.HashPassword(command.NewPassword);
        user.UpdatePasswordHash(hashedPassword);
        
        await unitOfWork.CompleteAsync();
    }
}