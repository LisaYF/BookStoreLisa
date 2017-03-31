using Lisa.BookStoreLisa.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Lisa.BookStoreLisa.web.Controllers
{
    [Authorize]
    public class ShopCartController : Controller
    {
        private BookStoreDB _db = new BookStoreDB();
        // GET: ShopCart
        public ActionResult Index()
        {
           var list= _db.Carts.Where(
                c => c.CartId ==User.Identity.Name).OrderByDescending(
                c=>c.DateCreated).ToList();
            
            decimal? totle = 0;
            foreach (var item in list)
            {
                
                totle += item.Book.Price * (int)item.Count;
            }
            ViewBag.TotalPrice = totle;
            
           return View(list);
        }
        public ActionResult UpdateCount(int? recordID,int? count)
        {
            var item = _db.Carts.SingleOrDefault(c => c.RecordId == recordID);
            item.Count = (int)count;
            _db.SaveChanges();
            var total = GetTotal();

            var result = new
            {
                Status = 1,
                TotalPrice = total.Item1,
                TotalCount = total.Item2
            };
            return Json(result);
        }
        public ActionResult AddToCart(int? bookID,int? count)
        {
            if (bookID == null || count == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var book = _db.Books.Find(bookID);//Find方法通过主键查找记录
            if (book!=null)
            {
                var cart=_db.Carts.SingleOrDefault(
                    c=>c.BookId==bookID
                  &&c.CartId==User.Identity.Name);//Where返回的是一个集合
                if (cart != null)
                {
                    cart.Count += (int)count;
                }
                else
                {
                    var newCart = new Cart(){
                        BookId=bookID,
                        CartId=User.Identity.Name,
                        Count=(int)count,
                        DateCreated=DateTime.Now
                    };        
                    _db.Carts.Add(newCart);              
                }
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        [AllowAnonymous]
        public ActionResult GetShopCartSummary()
        {
            int? count = 0;
            if (User.Identity.IsAuthenticated)
            {
                var list = _db.Carts.Where(
                    c => c.CartId == User.Identity.Name).ToList();
            
                foreach (var item in list)
                {
                    count += item.Count;
                }
            }
       
            ViewBag.TotalCount = count;
            return PartialView("_ShopCartSummary");
        }
        [HttpPost]
        public ActionResult Delete(int? recordID)
        {
           if (recordID == null)
            {
                var result = new { Status = 0, RecordID = recordID };
            }
            var item = _db.Carts.SingleOrDefault(c => c.RecordId == recordID);
            if (item != null)
            {
                _db.Carts.Remove(item);
                _db.SaveChanges();

                var total = GetTotal();

                var result = new { Status = 1,
                TotalPrice= total.Item1,
                TotalCount= total.Item2
                };
                return Json(result);

            }
            else
            {
                var result = new { Status = 2};
                return Json(result);

            }
            //     return RedirectToAction("Index");//转向购物车首页
        }
        private Tuple<decimal, int> GetTotal()
        {
            var list = _db.Carts.Where(c => c.CartId == User.Identity.Name);
            decimal? price = 0;
            int? count = 0;
            foreach (var item in list)
            {
                price += item.Count * item.Book.Price;
                count += item.Count;
            }
            //Tuple<decimal, int> result = new Tuple<decimal, int>(price,count);

            return new Tuple<decimal, int>((decimal)price, (int)count);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}