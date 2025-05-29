using InventoryManagementAPI.DTOs;
using InventoryManagementAPI.Models;
using InventoryManagementAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using InventoryManagementAPI.Data;

namespace InventoryManagementAPI.Repositories
{
    public class LocalImageRepository : IImageRepository
    {

        private readonly IWebHostEnvironment WebHostEnvironment;
        private readonly IHttpContextAccessor HttpContextAccessor;
        private readonly ApplicationDbContext DbContext;
        public LocalImageRepository(IWebHostEnvironment webHostEnvironment,
        IHttpContextAccessor httpContextAccessor,
        ApplicationDbContext DbContext)
        {
            this.WebHostEnvironment = webHostEnvironment;
            this.HttpContextAccessor = httpContextAccessor;
            this.DbContext = DbContext;
        }


        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(WebHostEnvironment.WebRootPath, "images",
                $"{image.FileName}{image.FileExtension}");

            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            var urlFilePath = $"{HttpContextAccessor.HttpContext.Request.Scheme}://{HttpContextAccessor.HttpContext.Request.Host}{HttpContextAccessor.HttpContext.Request.PathBase}/images/{image.FileName}{image.FileExtension}";

            image.FilePath = urlFilePath;

            await DbContext.Images.AddAsync(image);
            await DbContext.SaveChangesAsync();

            return image;
        }
    }
}