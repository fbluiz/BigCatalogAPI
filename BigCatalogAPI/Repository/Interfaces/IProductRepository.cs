using APICatalogo.Repository;
using BigCatalogAPI.Models;

namespace BigCatalogAPI.Repository.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetProductForPrice();
    }
}
