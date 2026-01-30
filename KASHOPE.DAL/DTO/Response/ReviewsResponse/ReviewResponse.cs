using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOPE.DAL.DTO.Response.ReviewsResponse
{
    public class ReviewResponse
    {
        public string UserName { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
