using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Entities;
using Entities.DataBase.Models;
using Microsoft.IdentityModel.Tokens;
using Repositories.RepositoryInterfaces;
using Services.ServiceInterfaces;

namespace Services;

public class TokenService(IRefreshTokenRepository refreshTokenRepository, string secretTokenKey):ITokenService
{
    private readonly TokenValidationParameters _validationParameters = new()
                                                                       {
                                                                           ValidateIssuerSigningKey = true,
                                                                           IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretTokenKey)),
                                                                           ValidateIssuer = false,
                                                                           ValidateAudience = false
                                                                       };

    public string TryGenerateToken(int tokenExpireMinutes, string login, Role role)
    {
        ArgumentException.ThrowIfNullOrEmpty(login);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secretTokenKey);
        var tokenDescriptor = new SecurityTokenDescriptor
                              {
                                  Subject = new(new[]
                                                {
                                                    new Claim(ExtendedClaimTypes.Login, login),
                                                    new Claim(ClaimTypes.Role, role.ToString())
                                                }),

                                  Expires = DateTime.UtcNow.AddMinutes(tokenExpireMinutes),
                                  SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                              };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var accessToken = tokenHandler.WriteToken(token);

        return accessToken;
    }

    public string TryGetLoginFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var principal = tokenHandler.ValidateToken(token, _validationParameters, out _);
        var loginClaim = principal.FindFirst(ExtendedClaimTypes.Login);

        if (loginClaim != null)
            return loginClaim.Value;

        // Обработка ошибки, если логин не найден
        throw new InvalidOperationException("Login claim not found in token");
    }

    public Role TryGetRoleFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var principal = tokenHandler.ValidateToken(token, _validationParameters, out _);
        var roleClaim = principal.FindFirst(ClaimTypes.Role);

        if (roleClaim != null && Enum.TryParse<Role>(roleClaim.Value, out var role))
            return role;

        // Обработка ошибки, если роль не найдена или некорректна
        throw new InvalidOperationException("Invalid or missing role in token");
    }

    public async Task<bool> IsValidRefreshTokenAsync(string refreshToken)
    {
        var dbRefreshToken = await refreshTokenRepository.GetByTokenAsync(refreshToken);

        if (dbRefreshToken is null)
            return false;

        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            tokenHandler.ValidateToken(refreshToken, _validationParameters, out _);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task TryUpdateTokenInDataBaseAsync(string token, string newToken)
    {
        var tokenFromDb = await refreshTokenRepository.GetByTokenAsync(token);

        if (tokenFromDb is null)
            throw new InvalidOperationException("Token is not exist in database");

        tokenFromDb.Token = newToken;

        await refreshTokenRepository.UpdateAsync(tokenFromDb);
    }

    public async Task TryAddTokenToDataBaseAsync(string token)
    {
        var tokenFromDb = await refreshTokenRepository.GetByTokenAsync(token);

        if (tokenFromDb is not null)
            throw new InvalidOperationException("Token already exist in database");

        var newToken = new RefreshToken { Token = token };

        await refreshTokenRepository.AddAsync(newToken);
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
            refreshTokenRepository.Dispose();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await refreshTokenRepository.DisposeAsync();
        GC.SuppressFinalize(this);
    }

    ~TokenService() { Dispose(false); }
}