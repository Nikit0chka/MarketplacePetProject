using Entities.DataBase.Models;

namespace Products.Domain.Services
{
    public interface IProductsService
    {
        ICollection<Product> GetAllProducts();
        Product? GetProductById(int id);
    }
}
