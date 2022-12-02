using APICatalogo.Repository;
using BigCatalogAPI.Context;
using BigCatalogAPI.Models;
using BigCatalogAPI.Pagination;
using BigCatalogAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BigCatalogAPI.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(NorthwindContext _context) : base(_context) 
        { 
        }
        public async Task<IEnumerable<Product>> GetProductForPrice()
        {
            return await Get().OrderBy(c=>c.UnitPrice).ToListAsync();
        }

        public PagedList<Product> GetProducts(ProductsParameters productParameters)
        {
            //return Get()
            //   .OrderBy(c => c.ProductId)
            //   Skip((productParameters.PageNumber - 1) * productParameters.PageSize)
            //   .Take(productParameters.PageSize)
            //   .ToList();

            return PagedList<Product>.ToPagedList(Get().OrderBy(on => on.ProductId),
                productParameters.PageNumber, productParameters.PageSize);
        }
    }
}
