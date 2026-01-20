using KASHOPE.BLL.Services.Interfaces;
using KASHOPE.DAL.DTO.Request;
using KASHOPE.DAL.DTO.Request.ProductRequest;
using KASHOPE.DAL.DTO.Response;
using KASHOPE.DAL.DTO.Response.ProductResponse;
using KASHOPE.DAL.Models;
using KASHOPE.DAL.Repository.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
            var result = await _productRepository.CreateAsync(product);
        }

        public async Task<List<ProductResponse>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Adapt<List<ProductResponse>>();
        }

        public async Task<PagintedResponse<ProductUserResponse>> GetAllProductsForUserAsync(string lang = "en",int limit = 3 , int page = 1 , string? search = null , int? categoryId = null , decimal? MinPrice = null , decimal? MaxPrice = null , string? sortby = null , bool asc = true)
        {
            var query =  _productRepository.Query();
            if(search is not null)
            {
                query = query.Where(p => p.ProductTranslations.Any(t => t.Language == lang && t.Name.Contains(search)));
            }
            if(categoryId is not null)
            {
                query = query.Where(p => p.CategoryId == categoryId);
            }
            if(MinPrice is not null)
            {
                query = query.Where(p => p.Price >= MinPrice);
            }
            if(MaxPrice is not null)
            {
                query = query.Where(p => p.Price <= MaxPrice);
            }
            if(sortby is not null)
            {
                sortby = sortby.ToLower();
                if(sortby == "price")
                {
                    query =  asc ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price) ;
                }else if (sortby == "name")
                {
                    query = asc ? query.OrderBy(p => p.ProductTranslations.FirstOrDefault(t => t.Language == lang ).Name) : query.OrderByDescending(p => p.ProductTranslations.FirstOrDefault(t => t.Language == lang).Name);
                }else if(sortby == "rate")
                {
                    query = asc ? query.OrderBy(p => p.Rate) : query.OrderByDescending(p => p.Rate);
                }
            }
            if (sortby is null)
            {
                query = query.OrderBy(p => p.CreatedAt);
            }
            var totalCount = await query.CountAsync();
            var products = await query.Skip((page - 1) * limit).Take(limit).ToListAsync();
            var response= products.BuildAdapter().AddParameters("lang",lang).AdaptToType<List<ProductUserResponse>>();
            return new PagintedResponse<ProductUserResponse>
            {
                TotalCount = totalCount,
                Litmit = limit,
                page = page,
                Data = response
            };
        }

        public async Task<List<ProductDetailsUserResponse>> GetAllProductsDetailsForUserAsync(string lang = "en")
        {
            var products = await _productRepository.GetAllAsync();
            var response = products.BuildAdapter().AddParameters("lang", lang).AdaptToType<List<ProductDetailsUserResponse>>();
            return response;
        }
        public async Task<BaseResponse> DeleteProduct(int id)
        {
            var product = await _productRepository.FindByIdAsync(id);
            if(product is null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Product Not Found"
                };
            }
            await _productRepository.DeleteAsync(product);
            return new BaseResponse
            {
                Success = true,
                Message = "Product Deleted"
            };
        }
    }
}
