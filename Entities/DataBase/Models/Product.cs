namespace Entities.DataBase.Models;

public class Product
{
    public required string Name { get; set; }
    public required int Count { get; set; }
    public required double Price { get; set; }
    public required ICollection<ProductCategory> Categories { get; set; }
}