using Products.Domain.Services;

namespace Products.API.Consumers
{
    public class ProductsBaseConsumer
    {
        protected readonly IProductsService ProductsService;
        public ProductsBaseConsumer(IProductsService productsService)
        {
            ProductsService = productsService;
        }
    }
}
