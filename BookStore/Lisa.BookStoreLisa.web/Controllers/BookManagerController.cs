using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Lisa.BookStoreLisa.web.Models;
using X.PagedList.Mvc;
using X.PagedList;

namespace Lisa.BookStoreLisa.web.Controllers
{
    [Authorize(Roles ="Admin")]//登陆成功才可以访问
    public class BookManagerController : Controller
    {
        private BookStoreDB db = new BookStoreDB();

        // GET: BookManager
        public ActionResult Index(int page=1)
        {
            var books = db.Books.Include(
                b => b.Author).Include(
                b => b.Category).OrderBy(
                b=>b.BookId);//分页一定要有排序
            return View(books.ToPagedList(page,8));
        }

        // GET: BookManager/Details/5
        public ActionResult Details(int? id)//可以为空
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: BookManager/Create
        public ActionResult Create()
        {

            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorId", "Name");
            ViewBag.CategoryId = new SelectList(db.Categorys, "CategoryId", "Name");
            return View();
        }

        // POST: BookManager/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateInput(false)]// 不让他验证输入的文本
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookId,CategoryId,AuthorID,Title,Price,ImageUrl,Details")] Book book,
            HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                
                
                    string name = Guid.NewGuid().ToString() + imageFile.FileName;
                    //GUID唯一标识符
                    //获取路径
                    string pathName = Server.MapPath("~/Images/") + name;
                    //保存图片
                    imageFile.SaveAs(pathName);
                    book.ImageUrl = "/Images/" + name;

    
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorId", "Name", book.AuthorID);
            ViewBag.CategoryId = new SelectList(db.Categorys, "CategoryId", "Name", book.CategoryId);
            return View(book);
        }

        // GET: BookManager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorId", "Name", book.AuthorID);
            ViewBag.CategoryId = new SelectList(db.Categorys, "CategoryId", "Name", book.CategoryId);
            return View(book);
        }

        // POST: BookManager/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookId,CategoryId,AuthorID,Title,Price,ImageUrl,Details")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorId", "Name", book.AuthorID);
            ViewBag.CategoryId = new SelectList(db.Categorys, "CategoryId", "Name", book.CategoryId);
            return View(book);
        }

        // GET: BookManager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: BookManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
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
