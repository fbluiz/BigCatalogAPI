using BigCatalogAPI.Models;
using BigCatalogAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BigCatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("/products")]
        public async Task<ActionResult<List<Product>>> Get()
        {
            var products = await _productRepository.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await _productRepository.ProductById(id);
            if (product is null)
            {
                return NotFound("Product Invalid!");
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> NewProduct([FromBody] Product product)
        {
            var newProduct = await _productRepository.AddProduct(product);

            return Ok(newProduct);
        }

        [HttpPut]
        public async Task<ActionResult<Product>> UpdateProduct([FromBody] Product product)
        {
            var newProduct = await _productRepository.UpdateProduct(product);

            return Ok(newProduct);
        }

        [HttpDelete("{id}")]
        public ActionResult<Product> DeleteProduct (int id)
        {
            var product = _productRepository.DeleteProduct(id);
            return (product);
        }

    }
}
