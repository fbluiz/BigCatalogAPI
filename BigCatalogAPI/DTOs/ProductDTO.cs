namespace BigCatalogAPI.DTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public int SupplierId { get; set; }
        public int CategoryId { get; set; }
    }
}
