using Microsoft.AspNetCore.Mvc;
using MyProduct = Infrastructure.Models.Product;

namespace Product.API
{
    [Route("api/products")]
    [ApiController]
    public class ProductService : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(new MyProduct());
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(MyProduct product)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(MyProduct product)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(MyProduct product)
        {
            throw new NotImplementedException();
        }
    }
}
