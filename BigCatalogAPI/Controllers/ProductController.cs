using BigCatalogAPI.Models;
using BigCatalogAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BigCatalogAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            var products = _unitOfWork._productRepository.Get().ToList();

            if (products == null)
            {
                return NotFound("Product not found");
            }
            return products;
        }

        [HttpGet("lowprice")]
        public ActionResult<IEnumerable<Product>> GetProductsByPrice() 
        {
            return _unitOfWork._productRepository.GetProductForPrice().ToList();
        }

        [HttpGet("{id:int}", Name = "GetProductById")]
        public ActionResult<Product> Get(int id)
        {
            var produto = _unitOfWork._productRepository.GetById(p => p.ProductId == id);
            if (produto is null)
            {
                return NotFound("Product not found");
            }
            return produto;
        }

        [HttpPost]
        public ActionResult Post(Product product)
        {
            if (product is null)
                return BadRequest();

            _unitOfWork._productRepository.Add(product);
            _unitOfWork.Commit();

            return new CreatedAtRouteResult("GetProductById",
                new { id = product.ProductId }, product);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _unitOfWork._productRepository.Update(product);
            _unitOfWork.Commit();

            return Ok(product);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var product = _unitOfWork._productRepository.GetById(p => p.ProductId == id);
            
            if (product is null)
            {
                return NotFound("Product not found");
            }
            _unitOfWork._productRepository.Delete(product);
            _unitOfWork.Commit();

            return Ok(product);
        }

    }
}
