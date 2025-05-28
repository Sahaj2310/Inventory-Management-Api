using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventoryManagementAPI.Models;

namespace InventoryManagementAPI.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int QuantityInStock { get; set; }

        [Range(0, int.MaxValue)]
        public int LowStockThreshold { get; set; } = 10; // Default threshold

        //foreign keys
        public Guid CategoryId { get; set; }
        public Guid SupplierId { get; set; }

        public Category? Category { get; set; }
        public Supplier? Supplier { get; set; }

        public bool IsLowStock => QuantityInStock <= LowStockThreshold;
    }
}