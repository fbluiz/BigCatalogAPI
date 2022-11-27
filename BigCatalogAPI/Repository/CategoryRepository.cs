using BigCatalogAPI.Models;
using Microsoft.EntityFrameworkCore;
using APICatalogo.Repository;
using BigCatalogAPI.Context;
using System;

namespace APICatalogo.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository (NorthwindContext _context) : base(_context)
        {
        }

        public IEnumerable<Category> GetCategoryAndProduct()
        {
            return Get().Include(c => c.Products);
        }
    }
}
