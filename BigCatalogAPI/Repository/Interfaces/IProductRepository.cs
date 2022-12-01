using APICatalogo.Repository;
using BigCatalogAPI.Models;
using BigCatalogAPI.Pagination;

namespace BigCatalogAPI.Repository.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetProductForPrice();

        IEnumerable<Product> GetProducts(ProductsParameters productParameters);
    }
}
