using Microsoft.AspNetCore.Mvc;

using MyProduct = Infrastructure.Models.Product;

namespace Product.API.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<IActionResult> GetProductById(int id)
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
