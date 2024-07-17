using System.ComponentModel.DataAnnotations;

namespace Products.DAL.ContextModels
{
    public class ProductCategoryContext
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public ProductCategoryContext? ParentProductCategoryContext { get; set; }
    }
}
