using Entities.DataBase.Models;
using Products.DAL.ContextModels;

namespace Products.Domain.Utils
{
    public class ProductCategoryTransformers
    {
        public static ProductCategory? ToProductCategory(ProductCategoryContext? productCategoryContext)
        {
            if (productCategoryContext == null)
                return null;
            else
                return new ProductCategory()
                {
                    Name = productCategoryContext.Name,
                    ParentProductCategory = ToProductCategory(productCategoryContext.ParentProductCategoryContext) ?? null
                };
        }

        public static ProductCategoryContext? ProductCategoryContext(ProductCategory? productCategory)
        {
            if (productCategory == null)
                return null;
            else
                return new ProductCategoryContext()
                {
                    Name = productCategory.Name,
                    ParentProductCategoryContext = ProductCategoryContext(productCategory.ParentProductCategory) ?? null
                };
        }
    }
}
