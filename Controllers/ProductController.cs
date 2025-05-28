using AutoMapper;
using InventoryManagementAPI.Models;
using InventoryManagementAPI.DTOs;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagementAPI.Data;
using System;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using InventoryManagementAPI.Interfaces;
using InventoryManagementAPI.Repositories;

namespace InventoryManagementAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllProducts")]

        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            var products = await _productRepository.GetAllProductsAsync(new RequestDto());
            if (products == null || !products.Any())
                return NotFound("No products found.");
            return Ok(products);
        }


        // GET
        [HttpGet]
        public async Task<ActionResult> GetAllProducts([FromQuery] RequestDto request)
        {
            var products = await _productRepository.GetAllProductsAsync(request);
            if (products == null || !products.Any())
                return NotFound("No products found.");
            return Ok(products);
        }


        // GET
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(Guid id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
                return NotFound($"Product with ID {id} not found.");
            return Ok(product);
        }

        // POST
        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct(CreateProductDto createDto)
        {
            // Optional FK validation
            var categoryExists = await _productRepository.CategoryExistsAsync(createDto.CategoryId);
            var supplierExists = await _productRepository.SupplierExistsAsync(createDto.SupplierId);
            if (!categoryExists)
                return BadRequest($"Category with ID {createDto.CategoryId} does not exist.");
            if (!supplierExists)
                return BadRequest($"Supplier with ID {createDto.SupplierId} does not exist.");

            var product = await _productRepository.CreateProductAsync(createDto);
            if (product == null)
                return BadRequest("Failed to create product.");
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);


        }


        // PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, UpdateProductDto updateDto)
        {
            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == updateDto.CategoryId);
            var supplierExists = await _context.Suppliers.AnyAsync(s => s.Id == updateDto.SupplierId);

            if (!categoryExists)
                return BadRequest($"Category with ID {updateDto.CategoryId} does not exist.");
            if (!supplierExists)
                return BadRequest($"Supplier with ID {updateDto.SupplierId} does not exist.");

            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            _mapper.Map(updateDto, product);

            await _context.SaveChangesAsync();

            return NoContent();
        }


        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            await _productRepository.DeleteProductAsync(id);
            return NoContent();
        }

        // GET
        [HttpGet("lowstock")]

        public async Task<ActionResult<IEnumerable<ProductDto>>> GetLowStockProducts()
        {
            var lowStockProducts = await _productRepository.GetLowStockProductsAsync();
            if (lowStockProducts == null || !lowStockProducts.Any())
                return NotFound("No low stock products found.");
            return Ok(lowStockProducts);
        }
    }
}
