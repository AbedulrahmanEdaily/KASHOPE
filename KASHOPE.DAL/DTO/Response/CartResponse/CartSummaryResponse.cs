using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOPE.DAL.DTO.Response.CartResponse
{
    public class CartSummaryResponse
    {
        public List<CartResponse> Items { get; set; }
        public decimal TotalCart => Items?.Sum(i => i.TotalPrice) ?? 0;
    }
}
