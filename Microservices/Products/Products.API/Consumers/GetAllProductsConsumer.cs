using MassTransit;
using Products.API.Requests;
using Products.Domain.Services;

namespace Products.API.Consumers
{
    public class GetAllProductsConsumer : ProductsBaseConsumer, IConsumer<GetAllProductRequest>
    {
        public GetAllProductsConsumer(IProductsService productsService) : base(productsService)
        {
        }

        public async Task Consume(ConsumeContext<GetAllProductRequest> context)
        {
            var products = ProductsService.GetAllProducts();
            //await context.RespondAsync(products);
        }
    }
}
