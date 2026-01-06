using KASHOPE.BLL.Services.Interfaces;
using KASHOPE.DAL.DTO.Request.ProductRequest;
using KASHOPE.DAL.DTO.Response.ProductResponse;
using KASHOPE.DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOPE.BLL.Services.Classes
{
    public class FileService : IFileService
    {
        public async Task<string?> UploadAsync(IFormFile file)
        {
            if(file != null && file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "images", fileName);
                using (var stream = File.Create(path))
                {
                    await file.CopyToAsync(stream);
                }
                return fileName;
            }
            return null;
        }
        public async Task<List<ProductImageResponse>?> UploadAsync(List<IFormFile> files)
        {
            var fileNames = new List<ProductImageResponse>();
            foreach(var file in files)
            {
                
                if(file != null && file.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "images", fileName);
                    using (var stream = File.Create(path))
                    {
                        await file.CopyToAsync(stream);
                    }
                    fileNames.Add(new ProductImageResponse { Image = fileName });
                }
            }
            return fileNames;
        }
    }
}
