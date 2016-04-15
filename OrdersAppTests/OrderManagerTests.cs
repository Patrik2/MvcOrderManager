using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace OrdersApp.Tests
{
    public static class TestOfProperties
    {
        public static void TestOfElements(this CartItem cartItem, Order actualOrder)
        {
            var actualOrderItem = actualOrder.OrderItems.First(orderItem => orderItem.Product.ItemId == cartItem.ItemId);
            Assert.AreEqual(cartItem.Quantity, actualOrderItem.Quantity);
            Assert.AreEqual(cartItem.Quantity * actualOrderItem.Price, actualOrderItem.TotalPrice);
        }
    }
    [TestClass]
    public class OrderManagerTests 
    {
        private Mock<IDbSet<T>> CreateMockedDbSet<T>   (IEnumerable<T> elements)
            where T : class
        {
            var queryable = elements.AsQueryable();
            var mockProducts = new Mock<IDbSet<T>>();
            mockProducts.Setup(m => m.Provider).Returns(queryable.Provider);
            mockProducts.Setup(m => m.Expression).Returns(queryable.Expression);
            mockProducts.Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockProducts.Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
            return mockProducts;
        }




        [TestMethod]
        public void CreateOrderTest_Passing4cartItems()
        {
            //ARRANGE
            CartItem cartItem1 = new CartItem { Quantity = 1, ItemId = 1 };
            CartItem cartItem2 = new CartItem { Quantity = 5, ItemId = 2 };
            CartItem cartItem3 = new CartItem { Quantity = 8, ItemId = 3 };

            CartItem[] cartItems = { cartItem1, cartItem2, cartItem3 };

            var products = new []
            {
                new Product {ItemId = 1, ItemPrice = 13.25 },
                new Product {ItemId = 2, ItemPrice = 4  },
                new Product {ItemId = 3, ItemPrice = 8  }
            };

            var mockProducts = CreateMockedDbSet(products);
            var mockIOrderContext = new Mock<IOrderContext>();
            mockIOrderContext.Setup(orderContext => orderContext.Products).Returns(mockProducts.Object);
            var orderManager = new OrderManager(mockIOrderContext.Object);

            //ACT
            var actualOrder = orderManager.CreateOrder(cartItems);

            //ASSERT

            

            /*foreach (var cartItem in cartItems)
            {
                var actualOrderItem = actualOrder.OrderItems.First(orderItem => orderItem.Product.ItemId == cartItem.ItemId);
                Assert.AreEqual(cartItem.Quantity, actualOrderItem.Quantity);
                Assert.AreEqual(cartItem.Quantity * actualOrderItem.Price ,actualOrderItem.TotalPrice);
            }*/

            //bool a = null;
            Assert.IsNotNull(actualOrder);
            Assert.IsNotNull(actualOrder.OrderItems);
            Assert.AreEqual(actualOrder.OrderItems.Count, cartItems.Length);
            cartItem1.TestOfElements(actualOrder);
            cartItem2.TestOfElements(actualOrder);
            cartItem3.TestOfElements(actualOrder);

            //Assert.IsNotNull(actualOrder.OrderItems.Single(x => x.Product.ItemId == cartItem1.ItemId && x.Quantity == cartItem1.Quantity));
            //Assert.IsTrue(actualOrder.OrderItems.Any(x => x.TotalPrice == 8m));
            //Assert.IsNotNull(actualOrder.OrderItems.Single(x => x.TotalPrice == 40m && x.Price == 8 && x.Quantity == 5));
            //Assert.IsTrue(actualOrder.OrderItems.Any(x => x.Quantity == 2));
            //Assert.IsNotNull(actualOrder.OrderItems.Single(x => x.Price == 13.25m));
            Assert.AreEqual(actualOrder.Created.Date, DateTime.Now.Date);
            

            
        }

        [TestMethod]
        [ExpectedException(typeof(OrderCreationException))]
        public void CreateOrderTest2_PassingEmptyArrayProductShouldThrowOrderCreationException()
        {
            //ARRANGE
            CartItem cartItem1 = new CartItem { Quantity = 1, ItemId = 1 };
           
            CartItem[] cartItems = { cartItem1};

            var products = new Product[0];

            var mockProducts = CreateMockedDbSet(products);

            var mockIOrderContext = new Mock<IOrderContext>();
            mockIOrderContext.Setup(orderContext => orderContext.Products).Returns(mockProducts.Object);
            var orderManager = new OrderManager(mockIOrderContext.Object);

            //ACT
            var actualOrder = orderManager.CreateOrder(cartItems);
        }

        [TestMethod]
        [ExpectedException(typeof(OrderCreationException))]
        public void CreateOrderTest2_PassingCartItemthatNotExistInProductsShouldThrowOrderCreationException()
        {
            //ARRANGE
            CartItem cartItem1 = new CartItem { Quantity = 1, ItemId = 1 };
            CartItem cartItem2 = new CartItem { Quantity = 2, ItemId = 2 };
            CartItem cartItem3 = new CartItem { Quantity = 2, ItemId = 4 };
            CartItem cartItem4 = new CartItem { Quantity = 5, ItemId = 3 };
            CartItem[] cartItems = { cartItem1, cartItem2, cartItem3, cartItem4 };

            var products = new []
            {
                new Product {ItemId = 1, ItemPrice = 13.25 },
                new Product {ItemId = 2, ItemPrice = 4  },
                new Product {ItemId = 3, ItemPrice = 8  }
            };

            var mockProducts = CreateMockedDbSet(products);

            var mockIOrderContext = new Mock<IOrderContext>();
            mockIOrderContext.Setup(orderContext => orderContext.Products).Returns(mockProducts.Object);
            var orderManager = new OrderManager(mockIOrderContext.Object);

            //ACT
            var actualOrder = orderManager.CreateOrder(cartItems);
        }
    }
}