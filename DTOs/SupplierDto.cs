using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventoryManagementAPI.Models;

namespace InventoryManagementAPI.DTOs
{
    public class SupplierDto
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? ContactInfo { get; set; }
    }
}
