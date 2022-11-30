using AutoMapper;
using BigCatalogAPI.DTOs;
using BigCatalogAPI.Models;
using BigCatalogAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace BigCatalogAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }
        [HttpGet("products")]
        public ActionResult<IEnumerable<CategoryDTO>> GetCategoriesProducts()
        {
            try
            {
                var categories = _uof._categoryRepository.GetCategoryAndProduct().ToList();
                
                var categoriesDto = _mapper.Map<List<CategoryDTO>>(categories);
                return categoriesDto;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "There was a problem handling your request.");
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoryDTO>> Get() 
        {
            try
            {
                var categories = _uof._categoryRepository.Get().ToList();

                var categoriesDto = _mapper.Map<List<CategoryDTO>>(categories);
                return categoriesDto;
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "There was a problem handling your request.");
            }
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public ActionResult<CategoryDTO> Get(int id)
        {
            try
            {
                var category = _uof._categoryRepository.GetById(p => p.CategoryId == id);

                var categoryDto = _mapper.Map<CategoryDTO>(category);
                
                if (category == null)
                {
                    return NotFound("category not found");
                }
                return Ok(categoryDto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "There was a problem handling your request.");
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] CategoryDTO categoryDto)
        {
            try
            {
                if (categoryDto is null)
                    return BadRequest("Invalid form for category");

                var category = _mapper.Map<Category>(categoryDto);
                _uof._categoryRepository.Add(category);
                _uof.Commit();

                var categoryDTo = _mapper.Map<CategoryDTO>(category);
                return new CreatedAtRouteResult("GetCategory",
                    new { id = category.CategoryId }, categoryDTo);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "There was a problem handling your request.");
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, [FromBody] CategoryDTO categoryDto)
        {
            try
            {
                if (id != categoryDto.CategoryId)
                {
                    return BadRequest("invalid data");
                }

                var category = _mapper.Map<Category>(categoryDto);

                _uof._categoryRepository.Update(category);
                _uof.Commit();

                var categoryDTo = _mapper.Map<CategoryDTO>(category);
                return Ok(categoryDTo);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "There was a problem handling your request.");
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult<ProductDTO> Delete(int id)
        {
            try
            {
                var category = _uof._categoryRepository.GetById(p => p.CategoryId == id);

                if (category == null)
                {
                    return NotFound("category not found");
                }
                _uof._categoryRepository.Delete(category);
                _uof.Commit();

                var categoryDto = _mapper.Map<CategoryDTO>(category);
                return Ok(categoryDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "There was a problem handling your request.");
            }
        }
    }
}
