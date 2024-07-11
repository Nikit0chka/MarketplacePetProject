using Entities;

namespace Services.ServiceInterfaces;

public interface ITokenService:IDisposable, IAsyncDisposable
{
    string TryGenerateToken(int tokenExpireMinutes, string login, Role role);
    string TryGetLoginFromToken(string token);
    Role TryGetRoleFromToken(string token);
    Task<bool> IsValidRefreshTokenAsync(string refreshToken);
    Task TryUpdateTokenInDataBaseAsync(string token, string newToken);
    Task TryAddTokenToDataBaseAsync(string token);

}