using System;

namespace InventoryManagementAPI.DTOs
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public int LowStockThreshold { get; set; }
        public bool IsLowStock { get; set; }
        
        public Guid CategoryId { get; set; }
        public Guid SupplierId { get; set; }
        
        public CategoryDto? Category { get; set; }
        public SupplierDto? Supplier { get; set; }
    }
}
