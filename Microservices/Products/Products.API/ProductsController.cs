using Entities.DataBase.Models;
using Infrastructure;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Products.API.Requests;

namespace Products.API
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {

        private readonly IBusControl _busControl;
        private readonly Uri _rabbitMqUrl = new Uri("rabbitmq://localhost/productsQueue");

        public ProductsController(IBusControl busControl)
        {
            _busControl = busControl;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var response = await GetResponseRabbitTask<GetAllProductRequest, ICollection<Product>>(new GetAllProductRequest());
            return Ok(ApiResult<ICollection<Product>>.Success200(response));
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(Product product)
        {
            throw new NotImplementedException();
        }

        private async Task<TOut> GetResponseRabbitTask<TIn, TOut>(TIn request)
            where TIn : class
            where TOut : class
        {
            var client = _busControl.CreateRequestClient<TIn>(_rabbitMqUrl);
            var response = await client.GetResponse<TOut>(request);
            return response.Message;
        }
    }
}
