using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lisa.BookStoreLisa.web.Models;
using X.PagedList.Mvc;
using X.PagedList;
using System.Net;

namespace Lisa.BookStoreLisa.web.Controllers
{
    public class BookController : Controller
    {
        private BookStoreDB db = new BookStoreDB();
        // GET: Book
        public ActionResult Index( int? pageNumber)//显示所有书的列表 ?可以为空
        {
            //string  sql = "select top 30 *(select top @pageNumber*30 * from Books orderby BookID) orderby BookID desc";//一页显示10条数据
            if (pageNumber == null)
            {
                pageNumber = 1;
            }
           
                IPagedList<Book> list = db.Books.OrderByDescending(
                p => p.BookId).ToPagedList((int)pageNumber, 4);//分页查询语句,一页几个

                return View(list);
            

        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
                Book book=db.Books.Find(id);
            //book.Author.Name
                 if (book != null)
                {
                    return View(book);
                }
                else
                {
                    return HttpNotFound();//404没有找到
                }
            
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
         
            base.Dispose(disposing);
        }
    }
}