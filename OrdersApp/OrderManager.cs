using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace OrdersApp
{
    public interface IOrderContext
    {
         IDbSet<Product> Products { get; set; }
         IDbSet<Order> Orders { get; set; }
         IDbSet<OrderItem> OrderItems { get; set; }
    }

    //public class VreceBezNicoho : IOrderContext
    //{
    //    public DbSet<OrderItem> OrderItems
    //    {
    //         get {
    //            return 
    //        }
    //        set { }
    //    }
    //    public DbSet<Order> Orders
    //    {
    //        get
    //        {
    //           return 
    //        }
    //        set
    //        {
                
    //        }
    //    }
    //    public DbSet<Product> Products
    //    {
    //        get
    //        {
    //            throw new NotImplementedException();
    //        }
    //        set
    //        {
                
    //        }
    //    }
    //}
    public class OrderManager 
    {
        private IOrderContext orderContext;
        public OrderManager(IOrderContext orderContext)
        {
            this.orderContext = orderContext;
        }
        public Order CreateOrder(IEnumerable<CartItem> cartItems)
        {
            Order order = new Order
            {
                OrderNumber = Guid.NewGuid().ToString(),
                Created = DateTime.Now,
                OrderItems = new List<OrderItem>()
            };
            foreach (var cartItem in cartItems)
            {
                Product product = SearchProduct(cartItem.ItemId);
                if (product == null)
                {
                    throw new OrderCreationException("Na sklade už požadovaný tovar nie je!!");
                }
                OrderItem orderItem = new OrderItem()
                {
                    Quantity = cartItem.Quantity,
                    Price = (decimal)product.ItemPrice,
                    TotalPrice = (decimal)product.ItemPrice * cartItem.Quantity,
                    OrderNumber = order.OrderNumber,
                    Product = product
                };
                order.OrderItems.Add(orderItem);
            }
            return order;
        }
        public Product SearchProduct(int productId)
        {
            var product = (from p in orderContext.Products
                           where p.ItemId == productId
                           select p).SingleOrDefault();
            return product;
        }
    }
}