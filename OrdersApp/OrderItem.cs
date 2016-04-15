using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OrdersApp
{
    public class OrderItem
    {
        [Key]
        public int OrderItemID { get; set; }
        public string OrderNumber { get; set; } 
        public Product Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        //public int ItemId { get; set; }
    }
}