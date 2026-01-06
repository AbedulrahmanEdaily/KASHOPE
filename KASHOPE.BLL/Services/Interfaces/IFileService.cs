using KASHOPE.DAL.DTO.Request.ProductRequest;
using KASHOPE.DAL.DTO.Response.ProductResponse;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOPE.BLL.Services.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadAsync(IFormFile file);
        Task<List<ProductImageResponse>?> UploadAsync(List<IFormFile> files);
    }
}
