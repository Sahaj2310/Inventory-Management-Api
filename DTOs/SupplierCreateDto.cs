using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventoryManagementAPI.Models;

namespace InventoryManagementAPI.DTOs
{
    public class SupplierCreateDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(250)]
        public string? ContactInfo { get; set; }
    }
}
