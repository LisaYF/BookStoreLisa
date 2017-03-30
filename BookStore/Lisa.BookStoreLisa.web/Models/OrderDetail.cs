namespace Lisa.BookStoreLisa.web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderDetailsId { get; set; }

        public int? OrderId { get; set; }

        public int? BookId { get; set; }

        public int? Quantity { get; set; }

        public decimal? UnitPrice { get; set; }

        public virtual Book Book { get; set; }

        public virtual Order Order { get; set; }
    }
}
