using Entities.DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities.DataBase;

public class RefreshTokenContext(DbContextOptions<RefreshTokenContext> options):DbContext(options)
{
    public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

}