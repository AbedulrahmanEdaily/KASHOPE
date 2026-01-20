using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOPE.DAL.Models
{
    public enum OrderStatus
    {
        Pending = 1,
        Cancelled = 2,
        Approved = 3,
        Shipped = 4,
        Delivered = 5
    }
    public enum PaymentMethod
    {
        Visa=2,Cash=1
    }
    public class Order
    {
        public int Id { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public DateTime? ShippedDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string? SessionId { get; set; }
        public string? PaymentId { get; set; }
        public decimal AmountPaid { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
