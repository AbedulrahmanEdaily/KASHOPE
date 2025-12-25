using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOPE.DAL.Models
{
    public class Product: BaseModel
    {
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal Quantity { get; set; }
        public double Rate { get; set; }
        public string? MainImage { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<ProductTranslation> ProductTranslations { get; set; }
    }
}
