using BigCatalogAPI.Models;
using System.Collections.Generic;

namespace APICatalogo.Repository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetCategoryAndProduct();
    }
}

