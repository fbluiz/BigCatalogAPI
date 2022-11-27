using BigCatalogAPI.Models;
using System.Collections.Generic;

namespace APICatalogo.Repository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<Category> GetCategoryAndProduct();
    }
}

