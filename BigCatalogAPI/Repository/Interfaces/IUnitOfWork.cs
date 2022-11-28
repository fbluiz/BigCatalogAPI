using APICatalogo.Repository;

namespace BigCatalogAPI.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository _productRepository {get;}
        ICategoryRepository _categoryRepository {get;}

        void Commit();
    }
}
