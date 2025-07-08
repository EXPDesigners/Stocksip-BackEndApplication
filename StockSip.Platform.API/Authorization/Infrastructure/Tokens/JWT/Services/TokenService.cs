using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using StockSip.Platform.API.Authorization.Application.Internal.OutboundServices.Token;
using StockSip.Platform.API.Authorization.Domain.Model.Aggregate;
using StockSip.Platform.API.Authorization.Infrastructure.Tokens.JWT.Configuration;

namespace StockSip.Platform.API.Authorization.Infrastructure.Tokens.JWT.Services;

/// <summary>
/// The token service is responsible for generating and validating JWT tokens.
/// </summary>
public class TokenService(IOptions<TokenSettings> tokenSettings) : ITokenService
{
    private readonly TokenSettings _tokenSettings = tokenSettings.Value;
    
    /// <summary>
    /// Generate a JWT token for the given user.
    /// </summary>
    /// <param name="user">The user for whom the token is generated.</param>
    /// <returns>The generated JWT token as a string.</returns>
    public string GenerateToken(User user)
    {
        var secret = _tokenSettings.Secret;
        var key = Encoding.ASCII.GetBytes(secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Sid, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var tokenHandler = new JsonWebTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return token;
    }

    /// <summary>
    /// Verify the validity of a JWT token and return the user ID if valid.
    /// </summary>
    /// <param name="token">The token to validate.</param>
    /// <returns>The user ID if the token is valid; otherwise, null.</returns>
    public async Task<string?> ValidateToken(string token)
    {
        // If token is null or empty
        if (string.IsNullOrEmpty(token))
            // Return null 
            return null;
        // Otherwise, perform validation
        var tokenHandler = new JsonWebTokenHandler();
        var key = Encoding.ASCII.GetBytes(_tokenSettings.Secret);
        try
        {
            var tokenValidationResult = await tokenHandler.ValidateTokenAsync(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // Expiration without delay
                ClockSkew = TimeSpan.Zero
            });

            var jwtToken = (JsonWebToken)tokenValidationResult.SecurityToken;
            var userId = jwtToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;;
            return userId;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}