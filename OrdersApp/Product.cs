using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OrdersApp
{
    public class Product
    {
        [Key]
        public int ItemId { get; set; }
        public double ItemPrice { get; set; }
    }
}
