using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOPE.DAL.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public string Comment { get; set; }
        public double Rating { get; set; }
        public DateTime CreatedTime { get;} = DateTime.UtcNow;
        public ApplicationUser User { get; set; }
        public Product Product { get; set; }
    }
}
