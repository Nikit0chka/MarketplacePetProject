using Entities.DataBase.Models;

namespace Repositories.RepositoryInterfaces;

public interface IRefreshTokenRepository:IDisposable, IAsyncDisposable
{
    Task<RefreshToken?> GetByIdAsync(int id);
    Task<RefreshToken?> GetByTokenAsync(string token);
    Task AddAsync(RefreshToken token);
    Task UpdateAsync(RefreshToken token);
    Task DeleteAsync(RefreshToken token);
}