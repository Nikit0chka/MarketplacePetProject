using System.ComponentModel.DataAnnotations;

namespace Entities.DataBase.Models;

public class RefreshToken
{
    public int Id { get; set; }

    [MaxLength(1000)]
    public required string Token { get; set; }
}