namespace Lisa.BookStoreLisa.web.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    //数据库的访问
    public partial class BookStoreDB : DbContext//对数据库进行操作的类
    {
        public BookStoreDB()
            : base("name=BookStoreDB")
        {
        }
        //: base表示调用基类的构造函数，把连接字符串传递过去
        //不写 就以类名命名的连接字符串

        //每个表
        //<>表示泛型 把数据库映射到类
        //ORM框架
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Category> Categorys { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(e => e.Total)
                .HasPrecision(18, 0);

            //modelBuilder.Entity<OrderDetails>()
            //    .Property(e => e.UnitPrice)
            //    .HasPrecision(18, 0);
        }
    }
}
