using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OrdersApp;

namespace MvcTry1.Controllers
{
    public class ProductsController : Controller
    {
        private OrderContext orderContext = new OrderContext();

        // GET: Products
        public ActionResult Index()
        {
            return View(orderContext.Products.ToList());
        }

        List<CartItem> StoredCartItems
        {
            get
            {
                return (List<CartItem>)Session["cart"];
            }
            set
            {
                Session["cart"] = value;
            }
        }
        public ActionResult Cart()
        {
            return View(StoredCartItems);
        }

        [HttpPost]
        public ActionResult Cart(List<int> quantities, List<int> ids)
        {
            var cartItems = new List<CartItem>();
            for(int i = 0; i < quantities.Count; i++)
            {
                if(quantities[i] == 0)
                {
                    continue;
                }
                var cartItem = new CartItem();
                cartItem.ItemId = ids[i];
                cartItem.Quantity = quantities[i];
                cartItems.Add(cartItem);
            }
            StoredCartItems = cartItems;
            return View(StoredCartItems);
        }
        [HttpPost]
        public ActionResult CreateOrder()
        {
            OrderManager orderManager = new OrderManager(orderContext);
            var order = orderManager.CreateOrder(StoredCartItems);
            return View(order);
        }

        public ActionResult Remove(int? itemID)
        {
            var cartItem = (from cartitem in StoredCartItems
                            where itemID == cartitem.ItemId
                            select cartitem).Single();
            StoredCartItems.Remove(cartItem);


            return RedirectToAction("Cart");
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = orderContext.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemId,ItemPrice")] Product product)
        {
            if (ModelState.IsValid)
            {
                orderContext.Products.Add(product);
                orderContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int itemID)
        {
            var cartItem = (from cartitem in StoredCartItems
                            where itemID == cartitem.ItemId
                            select cartitem).Single();
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            return View(cartItem);
        }
        [HttpPost]
        public ActionResult Edit(CartItem cartItem)
        {
            var oldCartItem = (from cartitem in StoredCartItems
                               where cartItem.ItemId == cartitem.ItemId
                               select cartitem).Single();
            
            StoredCartItems.Remove(oldCartItem);
            StoredCartItems.Add(cartItem);
            return RedirectToAction("Cart");
        }



        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ItemId,Quantity")] CartItem cartItem)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //orderContext.Entry(cartItem).State = EntityState.Modified;
        //        //orderContext.SaveChanges();
        //        //var listCart = StoredCartItems;
        //        //var car = from p in listCart
        //        //          where cartItem.ItemId == 
        //        //listCart.Remove()

        //        //return RedirectToAction("Cart");
        //    }
        //    return View(cartItem);
        //}

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = orderContext.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = orderContext.Products.Find(id);
            orderContext.Products.Remove(product);
            orderContext.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                orderContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
