using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OrdersApp
{
    public class CartItem
    {
        [Range(0,9999)]
        public int Quantity { get; set; }

        public int ItemId { get; set; }
    }
}
