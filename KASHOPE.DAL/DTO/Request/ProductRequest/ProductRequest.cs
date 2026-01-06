using KASHOPE.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KASHOPE.DAL.DTO.Request.ProductRequest
{
    public class ProductRequest
    {
        public List<ProductTranslationRequest> ProductTranslations { get; set; }
        public Status Status { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public IFormFile? MainImage { get; set; }
        public List<IFormFile> SubImages { get; set; }
        public int CategoryId { get; set; }
    }
}
