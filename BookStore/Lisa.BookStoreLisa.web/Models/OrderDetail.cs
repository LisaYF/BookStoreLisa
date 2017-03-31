namespace Lisa.BookStoreLisa.web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderDetail
    {
        public int? OrderID { get; set; }

        public int? BookId { get; set; }

        public int? Count { get; set; }

        [Key]
        public int OrderDetailsId { get; set; }

        public virtual Book Book { get; set; }
    }
}
