using KASHOPE.BLL.Services.Interfaces;
using KASHOPE.DAL.DTO.Request.ProductRequest;
using KASHOPE.DAL.DTO.Response.ProductResponse;
using KASHOPE.DAL.Models;
using KASHOPE.DAL.Repository.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOPE.BLL.Services.Classes
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileService _fileService;

        public ProductService(IProductRepository productRepository,IFileService fileService)
        {
            _productRepository = productRepository;
            _fileService = fileService;
        }

        public async Task CreateAsync(ProductRequest request)
        {
            var product = request.Adapt<Product>();
            if (request.MainImage != null)
            {
                product.MainImage = await _fileService.UploadAsync(request.MainImage);
            }
            if (request.SubImages != null)
            {
                var subImagePaths = await _fileService.UploadAsync(request.SubImages);
                product.SubImages = subImagePaths.Adapt<List<ProductImage>>();
            }
            var result = await _productRepository.AddAsync(product);
        }

        public async Task<List<ProductResponse>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Adapt<List<ProductResponse>>();
        }
    }
}
