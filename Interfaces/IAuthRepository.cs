using InventoryManagementAPI.DTOs;
using InventoryManagementAPI.Models;

namespace InventoryManagementAPI.Interfaces
{
    public interface IAuthRepository
    {
        Task<string> RegisterAsync(UserRegisterDto dto);
        Task<string> LoginAsync(UserLoginDto dto);
        string CreateToken(ApplicationUser user);
        Task<bool> CreateAdminUserAsync(string username, string password);
    }
}
