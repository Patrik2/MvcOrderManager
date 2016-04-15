using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OrdersApp
{
    public class Order
    {
        [Key]
        public string OrderNumber { get; set; }
        public DateTime Created { get; set; }
        public IList<OrderItem> OrderItems { get; set; }

    }
}
