namespace Lisa.BookStoreLisa.web.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    //���ݿ�ķ���
    public partial class BookStoreDB : DbContext//�����ݿ���в�������
    {
        public BookStoreDB()
            : base("name=BookStoreDB")
        {
        }
        //: base��ʾ���û���Ĺ��캯�����������ַ������ݹ�ȥ
        //��д �������������������ַ���

        //ÿ����
        //<>��ʾ���� �����ݿ�ӳ�䵽��
        //ORM���
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
