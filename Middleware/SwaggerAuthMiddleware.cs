using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System;

namespace InventoryManagementAPI.Middleware
{
    public class SwaggerAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public SwaggerAuthMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/swagger"))
            {
                var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                
                if (!string.IsNullOrEmpty(token))
                {
                    try
                    {
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
                        
                        tokenHandler.ValidateToken(token, new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(key),
                            ValidateIssuer = true,
                            ValidIssuer = _configuration["Jwt:Issuer"],
                            ValidateAudience = true,
                            ValidAudience = _configuration["Jwt:Audience"],
                            ClockSkew = TimeSpan.Zero
                        }, out SecurityToken validatedToken);

                        var jwtToken = (JwtSecurityToken)validatedToken;
                        var userRole = jwtToken.Claims.First(x => x.Type == "role").Value;
                        
                        context.Items["UserRole"] = userRole;
                    }
                    catch
                    {
                        // Token validation failed, treat as unauthenticated
                        context.Items["UserRole"] = null;
                    }
                }
                else
                {
                    context.Items["UserRole"] = null;
                }
            }

            await _next(context);
        }
    }
} 