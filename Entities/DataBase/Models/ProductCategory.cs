namespace Entities.DataBase.Models;

public class ProductCategory
{
    public required string Name { get; set; }
    public required ProductCategory? ParentProductCategory { get; set; }
}