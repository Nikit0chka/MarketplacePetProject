namespace Products.DAL.ContextModels
{
    public class ProductContext
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
        public ICollection<ProductCategoryContext>? Categories { get; set; }
    }
}
