using APICatalogo.Repository;
using BigCatalogAPI.Context;
using BigCatalogAPI.Models;
using BigCatalogAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BigCatalogAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _uof;

        public CategoryController(IUnitOfWork uof)
        {
            _uof = uof;
        }
        [HttpGet("products")]
        public ActionResult<IEnumerable<Category>> GetCategoriesProducts()
        {
            try
            {
                return _uof._categoryRepository.GetCategoryAndProduct().ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "There was a problem handling your request.");
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get() 
        {
            try
            {
                var categories = _uof._categoryRepository.Get().ToList();
                return categories;
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "There was a problem handling your request.");
            }
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public ActionResult<Category> Get(int id)
        {
            try
            {
                var category = _uof._categoryRepository.GetById(p => p.CategoryId == id);
                
                if (category == null)
                {
                    return NotFound("category not found");
                }
                return Ok(category);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "There was a problem handling your request.");
            }
        }

        [HttpPost]
        public ActionResult Post(Category category)
        {
            try
            {
                if (category is null)
                    return BadRequest("Invalid form for category");

                _uof._categoryRepository.Add(category);
                _uof.Commit();

                return new CreatedAtRouteResult("GetCategory",
                    new { id = category.CategoryId }, category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "There was a problem handling your request.");
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Category category)
        {
            try
            {
                if (id != category.CategoryId)
                {
                    return BadRequest("invalid data");
                }
                _uof._categoryRepository.Update(category);
                _uof.Commit();
                return Ok(category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "There was a problem handling your request.");
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
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
                return Ok(category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "There was a problem handling your request.");
            }
        }
    }
}
