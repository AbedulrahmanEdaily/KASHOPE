using KASHOPE.DAL.DTO.Response.ReviewsResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOPE.DAL.DTO.Response.ProductResponse
{
    public class ProductDetailsUserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public double Rate { get; set; }
        public string? MainImage { get; set; }
        public string Category { get; set; }
        public List<ProductImageResponse> SubImages { get; set; }
        public List<ReviewResponse> Reviews { get; set; }
    }
}
