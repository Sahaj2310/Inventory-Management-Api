using System.ComponentModel.DataAnnotations;

namespace InventoryManagementAPI.DTOs
{
    public class ImageUploadRequestDto
    {
        [Required]
        public IFormFile File { get; set; } = null!;

        [Required]
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
    }
}