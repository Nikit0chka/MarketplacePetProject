namespace Entities.DataBase.Models;

public class RefreshToken
{
    public int Id { get; set; }
    public required string Token { get; set; }
}