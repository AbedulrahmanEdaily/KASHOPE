using KASHOPE.DAL.DTO.Response.CartResponse;
using KASHOPE.DAL.DTO.Response.CategoryResponse;
using KASHOPE.DAL.DTO.Response.OrderResponse;
using KASHOPE.DAL.DTO.Response.ProductResponse;
using KASHOPE.DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Routing.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOPE.BLL.Mapster
{
    public static class MapsterConfiguration
    {
        public static void MapConfigCategory()
        {
            TypeAdapterConfig<Category, CategoryResponse>.NewConfig().Map(ds => ds.CreatedBy, sr => sr.User.UserName);
            TypeAdapterConfig<Category, CategoryUserResponse>.NewConfig().Map(ds => ds.Name, sr => sr.CategoryTranslations.Where(t=>t.Language == MapContext.Current.Parameters["lang"].ToString()).Select(t=>t.Name).FirstOrDefault());
            TypeAdapterConfig<Product, ProductResponse>.NewConfig()
                .Map(dest => dest.CreatedBy,
                     src => src.User.UserName)
                .Map(dest => dest.Category,
                     src => src.Category.CategoryTranslations
                         .Where(t => t.CategoryId == src.CategoryId)
                         .Select(t => t.Name)
                         .FirstOrDefault())
                .Map(dest => dest.MainImage,
                     src => $"https://localhost:7026/images/{src.MainImage}")
                .Map(dest => dest.SubImages,
                     src => src.SubImages.Select(img => new ProductImageResponse
                     {
                         Image = $"https://localhost:7026/images/{img.Image}"
                     }).ToList());
            TypeAdapterConfig<Product, ProductUserResponse>.NewConfig()
                .Map(dest => dest.Name,
                    src => src.ProductTranslations
                        .FirstOrDefault(t =>
                            t.Language == (MapContext.Current.Parameters["lang"].ToString() ?? "en")
                        )!.Name
                )
                .Map(dest => dest.MainImage,
                    src => $"https://localhost:7026/images/{src.MainImage}"
                )
                .Map(dest => dest.Category,
                    src => src.Category.CategoryTranslations
                        .FirstOrDefault(t =>
                            t.Language == (MapContext.Current.Parameters["lang"].ToString() ?? "en")
                        )!.Name
                );
            TypeAdapterConfig<Product, ProductDetailsUserResponse>.NewConfig()
                .Map(dest => dest.Name, src => src.ProductTranslations.Where(n => n.Language == MapContext.Current.Parameters["lang"].ToString()).Select(t => t.Name).FirstOrDefault())
                .Map(dest => dest.Description, src => src.ProductTranslations.Where(n => n.Language == MapContext.Current.Parameters["lang"].ToString()).Select(t => t.Description).FirstOrDefault())
                .Map(dest => dest.MainImage, src => $"https://localhost:7026/images/{src.MainImage}")
                .Map(dest => dest.Category, src => src.Category.CategoryTranslations.Where(n => n.Language == MapContext.Current.Parameters["lang"].ToString()).Select(t => t.Name).FirstOrDefault())
                .Map(dest => dest.SubImages, src => src.SubImages.Select(t => new ProductImage
                {
                    Image = $"https://localhost:7026/images/{t.Image}"
                }).ToList());
            TypeAdapterConfig<Order, OrderResponse>.NewConfig().Map(dest => dest.UserName, src => src.User.UserName);
        }
    }
}
