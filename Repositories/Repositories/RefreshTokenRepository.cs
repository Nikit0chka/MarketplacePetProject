using Entities.DataBase;
using Entities.DataBase.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.RepositoryInterfaces;

namespace Repositories.Repositories;

public class RefreshTokenRepository(RefreshTokenContext dbContext):
    IRefreshTokenRepository
{
    public async Task<RefreshToken?> GetByIdAsync(int id) => await dbContext.RefreshTokens.FindAsync(id);
    public async Task<RefreshToken?> GetByTokenAsync(string token) => await dbContext.RefreshTokens.FirstOrDefaultAsync(dbToken => dbToken.Token == token);

    public async Task AddAsync(RefreshToken token)
    {
        await dbContext.RefreshTokens.AddAsync(token);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(RefreshToken token)
    {
        dbContext.RefreshTokens.Update(token);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(RefreshToken token)
    {
        dbContext.RefreshTokens.Remove(token);
        await dbContext.SaveChangesAsync();
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
            dbContext.Dispose();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private async ValueTask DisposeAsyncCore() => await dbContext.DisposeAsync();

    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore();
        GC.SuppressFinalize(this);
    }

    ~RefreshTokenRepository() { Dispose(false); }
}