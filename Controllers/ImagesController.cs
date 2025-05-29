using AutoMapper;
using InventoryManagementAPI.Models;
using InventoryManagementAPI.DTOs;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagementAPI.Data;
using System;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IO;
using InventoryManagementAPI.Repositories;
using InventoryManagementAPI.Interfaces;

namespace InventoryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ImagesController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly IImageRepository _imageRepository;
        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }


        [HttpPost]
        [Route("upload")]

        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            ValidateFileUpload(request);
            if (ModelState.IsValid)
            {
                var imagemodel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName).ToLower(),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.FileName,
                    FileDescription = request.FileDescription,
                };

                await _imageRepository.Upload(imagemodel);
                return Ok(imagemodel);
            }
            
            return BadRequest(ModelState);
        }


        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExtension = Path.GetExtension(request.File.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
            {
                ModelState.AddModelError("File", "Unsupported file extensions");
            }

            if (request.File.Length > 4194304)
            {
                ModelState.AddModelError("File", "File size exceeds the maximum limit of 4 MB.");
            }
        }

    }
}