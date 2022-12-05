using APICatalogo.Repository;
using BigCatalogAPI.Models;
using BigCatalogAPI.Pagination;

namespace BigCatalogAPI.Repository.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductForPrice();

        Task<PagedList<Product>> GetProducts(ProductsParameters productParameters);
    }
}
