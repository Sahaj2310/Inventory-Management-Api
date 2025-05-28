using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagementAPI.DTOs;
using InventoryManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using InventoryManagementAPI.Data;
using InventoryManagementAPI.Interfaces;
using InventoryManagementAPI.Configurations;
using Microsoft.Extensions.Options;

namespace InventoryManagementAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly LowStockSettings _lowStockSettings;

        public ProductRepository(
            ApplicationDbContext context, 
            IMapper mapper,
            IOptions<LowStockSettings> lowStockSettings)
        {
            _context = context;
            _mapper = mapper;
            _lowStockSettings = lowStockSettings.Value;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync(RequestDto request)
        {
            IQueryable<Product> query = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier);

            if (!string.IsNullOrEmpty(request.Name))
                query = query.Where(p => p.Name.Contains(request.Name, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(request.Category))
                query = query.Where(p => p.Category.Name.Contains(request.Category, StringComparison.OrdinalIgnoreCase));

            if (request.MinPrice.HasValue)
                query = query.Where(p => p.Price >= request.MinPrice.Value);

            if (request.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= request.MaxPrice.Value);

            if (request.ShowLowStockOnly)
                query = query.Where(p => p.QuantityInStock <= p.LowStockThreshold);

            if (!string.IsNullOrEmpty(request.SortBy))
            {
                switch (request.SortBy.ToLower())
                {
                    case "price":
                        query = request.SortDescending ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price);
                        break;
                    case "name":
                        query = request.SortDescending ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name);
                        break;
                    case "stock":
                        query = request.SortDescending ? query.OrderByDescending(p => p.QuantityInStock) : query.OrderBy(p => p.QuantityInStock);
                        break;
                    default:
                        query = query.OrderBy(p => p.Name);
                        break;
                }
            }

            query = query.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize);

            var products = await query.ToListAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto> GetProductByIdAsync(Guid id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(p => p.Id == id);

            return product == null ? null : _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> CreateProductAsync(CreateProductDto createDto)
        {
            var product = _mapper.Map<Product>(createDto);
            product.Id = Guid.NewGuid();

            // Set default low stock threshold if not provided
            if (product.LowStockThreshold == 0)
                product.LowStockThreshold = _lowStockSettings.DefaultThreshold;

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<bool> UpdateProductAsync(Guid id, UpdateProductDto updateDto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return false;

            _mapper.Map(updateDto, product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ProductDto>> GetLowStockProductsAsync()
        {
            var products = await _context.Products
                .Where(p => p.QuantityInStock <= p.LowStockThreshold)
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .OrderBy(p => p.QuantityInStock)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<bool> CategoryExistsAsync(Guid categoryId)
        {
            return await _context.Categories.AnyAsync(c => c.Id == categoryId);
        }

        public async Task<bool> SupplierExistsAsync(Guid supplierId)
        {
            return await _context.Suppliers.AnyAsync(s => s.Id == supplierId);
        }
    }
}
