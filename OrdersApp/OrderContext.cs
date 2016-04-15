using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace OrdersApp
{
    public class OrderContext : DbContext, IOrderContext
    {
        public OrderContext()
           : base("name=OrderEntities")
        {

        }

        public IDbSet<Product> Products { get; set; }
        public IDbSet<Order> Orders { get; set; }
        public IDbSet<OrderItem> OrderItems { get; set; }
    }
}
