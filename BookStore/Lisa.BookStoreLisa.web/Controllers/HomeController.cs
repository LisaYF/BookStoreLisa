using Lisa.BookStoreLisa.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lisa.BookStoreLisa.web.Controllers
{
    public class HomeController : Controller
    {
        //private int Select(Book b)
        //{
        //    return b.Price;
        //}
        public ActionResult Index()
        {
            using (BookStoreDB db = new BookStoreDB())
            {
               List<Book> list = db.Books.OrderByDescending(p => p.BookId).Take(10).ToList();
               return View(list);
            }
            /*  object s = "Hello World";*///传string 会冲突
            //ViewBag.Hello = s;//动态类型可以任意添加
            // ViewData["Hello"] = s;//字典

            //BookStoreDB db = new BookStoreDB();
            //List<Book> list = db.Books.OrderByDescending(p => p.BookId).Take(10).ToList();
            ////db.Books.OrderByDescending(p => p.BookId).Take(10).ToList();//选择用哪一列来倒叙
            ////查询最新的10条数据

            //兰姆达表达式 这条语句就是一个方法//db.Books.OrderByDescending(Select);
            //return View(list);//基类 到View里面找 对用的Action方法
            ////传进model
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
//private bool Con(int item)
//{
//    return item > 5;
//}
//public delegate bool Cond( int item);
//public int[] Select()
//{
//    return result::::::
//}

//class Program
//{
//    static void Main(string[], args)
//    {
//        MyCollection c = new MyCollection();
//        int[] list = c.Select(p>=p< 5);
//    }
//}