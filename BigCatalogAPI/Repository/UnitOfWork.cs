using APICatalogo.Repository;
using BigCatalogAPI.Context;
using BigCatalogAPI.Repository.Interfaces;

namespace BigCatalogAPI.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ProductRepository productRepository;
        public CategoryRepository categoryRepository;
        public NorthwindContext context;

        public UnitOfWork(NorthwindContext context)
        {
            this.context = context;
        }

        public IProductRepository _productRepository
        {
            get
            {
                return productRepository = productRepository ?? new ProductRepository(context);
            }
        }

        public ICategoryRepository _categoryRepository
        {
            get
            {
                return categoryRepository = categoryRepository ?? new CategoryRepository(context);
            }
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }

    }
}
