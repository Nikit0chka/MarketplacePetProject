using Entities.DataBase.Models;
using Microsoft.AspNetCore.Mvc;

namespace Products.API
{
    [Route("api/products")]
    [ApiController]
    public class ProductService : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok();
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
    }
}
