using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.DynamicData;

namespace WebsiteBanCoffee.Models
{
    [TableName("OrderDetails")]
    public class OrderDetails
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        [Required]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public int Count { get; set; }
        public string size { get; set; }
    }
}