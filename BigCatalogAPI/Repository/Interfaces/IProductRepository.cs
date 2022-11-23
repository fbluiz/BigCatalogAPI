using BigCatalogAPI.Models;

namespace BigCatalogAPI.Repository.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> ProductById(int id);
        Task<Product> AddProduct (Product product);
        Task<Product> UpdateProduct(Product product);
        void DeleteProduct(Product product);

    }
}
