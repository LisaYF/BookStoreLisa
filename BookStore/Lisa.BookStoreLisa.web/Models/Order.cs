namespace Lisa.BookStoreLisa.web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        public int OrderID { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        public decimal? Total { get; set; }

        public DateTime? DateCreated { get; set; }
    }
}
