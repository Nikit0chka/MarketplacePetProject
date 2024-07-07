using Entities;

namespace Services.ServiceInterfaces;

public interface ITokenService:IDisposable, IAsyncDisposable
{
    string GenerateToken(string secretTokenKey, int tokenExpireMinutes, string email, Role role);
    string GetEmailFromToken(string token, string secretTokenKey);
    Role GetRoleFromToken(string token, string secretTokenKey);
    Task<bool> IsValidRefreshTokenAsync(string refreshToken, string secretTokenKey);
    Task TryAddTokenToDataBaseAsync(string token);
    
}