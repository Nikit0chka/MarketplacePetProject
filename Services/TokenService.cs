using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Entities;
using Entities.DataBase.Models;
using Microsoft.IdentityModel.Tokens;
using Repositories.RepositoryInterfaces;
using Services.ServiceInterfaces;

namespace Services;

public class TokenService(IRefreshTokenRepository refreshTokenRepository):ITokenService
{
    public string GenerateToken(string secretTokenKey, int tokenExpireMinutes, string email, Role role)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secretTokenKey);
        var tokenDescriptor = new SecurityTokenDescriptor
                              {
                                  Subject = new(new[]
                                                {
                                                    new Claim(ClaimTypes.Email, email),
                                                    new Claim(ClaimTypes.Role, role.ToString())
                                                }),

                                  Expires = DateTime.UtcNow.AddMinutes(tokenExpireMinutes),
                                  SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                              };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var accessToken = tokenHandler.WriteToken(token);

        return accessToken;
    }

    public string GetEmailFromToken(string token, string secretTokenKey)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
                                   {
                                       ValidateIssuerSigningKey = true,
                                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretTokenKey)),
                                       ValidateIssuer = false,
                                       ValidateAudience = false
                                   };


        var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
        var email = principal.FindFirst(ClaimTypes.Email)!.Value;

        return email;
    }

    public Role GetRoleFromToken(string token, string secretTokenKey)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
                                   {
                                       ValidateIssuerSigningKey = true,
                                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretTokenKey)),
                                       ValidateIssuer = false,
                                       ValidateAudience = false
                                   };


        var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
        var role = principal.FindFirst(ClaimTypes.Role)!.Value;

        var roleEnum = (Role) Enum.Parse(typeof(Role), role);

        return roleEnum;
    }

    public async Task<bool> IsValidRefreshTokenAsync(string refreshToken, string secretTokenKey)
    {
        var dbRefreshToken = await refreshTokenRepository.GetByTokenAsync(refreshToken);

        if (dbRefreshToken is null)
            return false;

        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
                                   {
                                       ValidateIssuerSigningKey = true,
                                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretTokenKey)),
                                       ValidateIssuer = false,
                                       ValidateAudience = false
                                   };

        try
        {
            tokenHandler.ValidateToken(refreshToken, validationParameters, out _);
            return true;
        }
        catch
        {
            return false;
        }
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