using KASHOPE.DAL.DTO.Response.CategoryResponse;
using KASHOPE.DAL.DTO.Response.ProductResponse;
using KASHOPE.DAL.Models;
using Mapster;
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
            TypeAdapterConfig<Product, ProductResponse>
            .NewConfig()
            .Map(dest => dest.CreatedBy,
                 src => src.User.UserName)
            .Map(dest => dest.Category,
                 src => src.Category.CategoryTranslations
                     .Where(t => t.Id == src.CategoryId)
                     .Select(t => t.Name)
                     .FirstOrDefault())
            .Map(dest => dest.MainImage,
                 src => $"https://localhost:7026/images/{src.MainImage}")
            .Map(dest => dest.SubImages,
                 src => src.SubImages.Select(img => new ProductImageResponse
                 {
                     Image = $"https://localhost:7026/images/{img.Image}"
                 }).ToList());
        }
    }
}
