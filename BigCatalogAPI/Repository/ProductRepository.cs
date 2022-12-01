using APICatalogo.Repository;
using BigCatalogAPI.Context;
using BigCatalogAPI.Models;
using BigCatalogAPI.Pagination;
using BigCatalogAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace BigCatalogAPI.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(NorthwindContext _context) : base(_context) 
        { 
        }
        public IEnumerable<Product> GetProductForPrice()
        {
            return Get().OrderBy(c=>c.UnitPrice).ToList();
        }

        public IEnumerable<Product> GetProducts(ProductsParameters productParameters)
        {
            return Get()
                .OrderBy(c => c.ProductId)
                .Skip((productParameters.PageNumber - 1) * productParameters.PageSize)
                .Take(productParameters.PageSize)
                .ToList();
        }
    }
}
