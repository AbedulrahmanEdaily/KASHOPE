using KASHOPE.DAL.DTO.Request.ProductRequest;
using KASHOPE.DAL.DTO.Request.ProductRespnse;
using KASHOPE.DAL.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KASHOPE.DAL.DTO.Response.ProductResponse
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public List<ProductTranslationResponse> ProductTranslations { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status Status { get; set; }
        public string CreatedBy { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public string? MainImage { get; set; }
        public List<ProductImageResponse?> SubImages { get; set; }
        public string Category { get; set; }
    }
}
