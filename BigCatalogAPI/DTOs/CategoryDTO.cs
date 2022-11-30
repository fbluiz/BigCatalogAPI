using BigCatalogAPI.Models;

namespace BigCatalogAPI.DTOs
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } 
        public string Description { get; set; }
        public virtual ICollection<ProductDTO> Products { get; set; } 
    }
}
