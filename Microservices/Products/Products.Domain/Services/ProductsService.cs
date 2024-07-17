using Entities.DataBase.Models;
using Products.DAL.Data;
using Products.Domain.Utils;

namespace Products.Domain.Services
{
    public class ProductsService : IProductsService
    {
        private ProductsDbContext _productDbContext;

        public ProductsService(ProductsDbContext productDbContext)
        {
            _productDbContext = productDbContext;
        }

        public ICollection<Product> GetAllProducts()
        {
            return _productDbContext.Products.Select(x => ProductTransformers.ToProduct(x)).ToList();
        }

        public Product? GetProductById(int id)
        {
            return ProductTransformers.ToProduct(_productDbContext.Products.Where(x => x.Id == id).First());
        }

        public void AddProduct(Product product)
        {
            _productDbContext.Products.Add(ProductTransformers.ToProductContext(product));
        }
    }
}
