namespace Entities.DataBase.Models;

public class Client
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public string? Patronymic { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Login { get; set; }
    public required string Password { get; set; }
    public required int Money { get; set; }
    public required ICollection<Product> ProductsInBasket { get; set; }
}