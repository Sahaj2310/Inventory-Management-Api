using InventoryManagementAPI.DTOs;
using InventoryManagementAPI.Models;

namespace InventoryManagementAPI.Interfaces
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
