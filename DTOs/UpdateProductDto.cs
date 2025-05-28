using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventoryManagementAPI.Models;

namespace InventoryManagementAPI.DTOs
{
    public class UpdateProductDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(500)]
        public string? Description { get; set; }
        [Range(0.01, 1000000)]
        public decimal Price { get; set; }
        [Range(0, 10000)]
        public int QuantityInStock { get; set; }
        [Range(0, 10000)]
        public int LowStockThreshold { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        [Required]
        public Guid SupplierId { get; set; }
    }
}