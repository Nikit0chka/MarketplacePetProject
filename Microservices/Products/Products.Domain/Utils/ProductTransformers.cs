using Entities.DataBase.Models;
using Products.DAL.ContextModels;

namespace Products.Domain.Utils
{
    public class ProductTransformers
    {
        public static Product? ToProduct(ProductContext? productContext)
        {
            if (productContext == null)
                return null;
            else
                return new Product()
                {
                    Name = productContext.Name,
                    Count = productContext.Count,
                    Price = productContext.Price,
                    Categories = new List<ProductCategory>(productContext.Categories.Select(x => ProductCategoryTransformers.ToProductCategory(x)))
                };
        }

        public static ProductContext? ToProductContext(Product? product)
        {
            if (product == null)
                return null;
            else
                return new ProductContext()
                {
                    Name = product.Name,
                    Count = product.Count,
                    Price = product.Price,
                    Categories = new List<ProductCategoryContext>(product.Categories.Select(x => ProductCategoryTransformers.ProductCategoryContext(x)))
                };
        }
    }
}
