using InventoryManagementAPI.DTOs;
using InventoryManagementAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using InventoryManagementAPI.Models;

namespace InventoryManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _auth;

        public AuthController(IAuthRepository auth)
        {
            _auth = auth;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto dto)
        {
            var result = await _auth.RegisterAsync(dto);
            if (result == "User already exists.")
                return BadRequest(result);
            if (result == "Invalid role. Must be either 'User' or 'Admin'.")
                return BadRequest(result);

            return Ok(new { token = result });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            var result = await _auth.LoginAsync(dto);
            if (result == "Invalid credentials")
                return Unauthorized(result);

            return Ok(new { token = result });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create-admin")]
        public async Task<IActionResult> CreateAdmin(UserLoginDto dto)
        {
            var result = await _auth.CreateAdminUserAsync(dto.Username, dto.Password);
            if (!result)
                return BadRequest("Username already exists or invalid input.");

            return Ok(new { message = "Admin user created successfully." });
        }
    }
}
