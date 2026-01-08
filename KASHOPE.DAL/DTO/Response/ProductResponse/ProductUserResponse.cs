using KASHOPE.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOPE.DAL.DTO.Response.ProductResponse
{
    public class ProductUserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public double Rate { get; set; }
        public string? MainImage { get; set; }
        public string Category { get; set; }
    }
}
