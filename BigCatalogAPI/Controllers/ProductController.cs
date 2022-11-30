using AutoMapper;
using BigCatalogAPI.DTOs;
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
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork uof, IMapper mapper)
        {
            _unitOfWork = uof;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> GetProducts()
        {
            var products = _unitOfWork._productRepository.Get().ToList();
            
            var productsDto = _mapper.Map<List<ProductDTO>>(products);
            return productsDto;
        }

        [HttpGet("lowprice")]
        public ActionResult<IEnumerable<ProductDTO>> GetProductsByPrice() 
        {
            var productsForPrice = _unitOfWork._productRepository.GetProductForPrice().ToList();
            var productsDto = _mapper.Map<List<ProductDTO>>(productsForPrice);

            return productsDto;
        }

        [HttpGet("{id:int}", Name = "GetProductById")]
        public ActionResult<ProductDTO> Get(int id)
        {
            var product = _unitOfWork._productRepository.GetById(p => p.ProductId == id);
            
            if (product == null)
            {
                return NotFound("Product not found");
            }
            
            var productDto = _mapper.Map<ProductDTO>(product);
            return productDto;
        }

        [HttpPost]
        public ActionResult Post([FromBody] ProductDTO productDto)
        {
            if (productDto is null)
                return BadRequest();

            var product = _mapper.Map<Product>(productDto);
            
            _unitOfWork._productRepository.Add(product);
            _unitOfWork.Commit();

            var productDTO = _mapper.Map<ProductDTO>(product);
            return new CreatedAtRouteResult("GetProductById",
                new { id = product.ProductId }, productDTO);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ProductDTO> Put(int id, [FromBody] ProductDTO productDTO)
        {
            if (id != productDTO.ProductId)
            {
                return BadRequest();
            }

            var product = _mapper.Map<Product>(productDTO);

            _unitOfWork._productRepository.Update(product);
            _unitOfWork.Commit();

            var productDto = _mapper.Map<ProductDTO>(product);
            return Ok(productDto);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<ProductDTO> Delete(int id)
        {
            var product = _unitOfWork._productRepository.GetById(p => p.ProductId == id);
            
            if (product is null)
            {
                return NotFound("Product not found");
            }
            _unitOfWork._productRepository.Delete(product);
            _unitOfWork.Commit();

            var productDto = _mapper.Map<ProductDTO>(product);

            return Ok(productDto);
        }

    }
}
