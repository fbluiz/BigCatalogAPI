using BigCatalogAPI.Context;
using BigCatalogAPI.Models;
using BigCatalogAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace BigCatalogAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly NorthwindContext _context;

        public ProductRepository(NorthwindContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products.AsNoTracking().ToListAsync();
        }

        public async Task<Product> ProductById(int id)
        {
            return await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.ProductId == id);
        }
        public async Task<Product> AddProduct(Product product)
        {
            await _context.AddAsync(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
             _context.Entry(product).State = EntityState.Modified;
             _context.Set<Product>().Update(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public Product DeleteProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(c => c.ProductId == id);
            _context.Products.Remove(product);
            _context.SaveChanges();
            return (product);
        }

    }
}
