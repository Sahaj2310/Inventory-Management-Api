using System.ComponentModel.DataAnnotations;

namespace InventoryManagementAPI.DTOs;

public class UserRegisterDto
{
    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    public string Password { get; set; } = string.Empty;

    public string Role { get; set; } = "User"; // Default role is User
}
