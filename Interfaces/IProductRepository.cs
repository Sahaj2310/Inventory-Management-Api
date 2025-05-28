using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagementAPI.DTOs;

namespace InventoryManagementAPI.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync(RequestDto request);
        Task<ProductDto> GetProductByIdAsync(Guid id);
        Task<ProductDto> CreateProductAsync(CreateProductDto createDto);
        Task<bool> UpdateProductAsync(Guid id, UpdateProductDto updateDto);
        Task<bool> DeleteProductAsync(Guid id);
        Task<bool> CategoryExistsAsync(Guid categoryId);
        Task<bool> SupplierExistsAsync(Guid supplierId);
        Task<IEnumerable<ProductDto>> GetLowStockProductsAsync();
    }
}
