using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.DynamicData;

namespace WebsiteBanCoffee.Models
{
    [TableName("Order")]
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [DisplayName("Order.OrderDate")]
        [Required]
        public DateTime? OrderDate { get; set; }

        [DisplayName("Order.ShippingDate")]
        public DateTime? ShippingDate { get; set; }

        [DisplayName("Order.OrderTotal")]
        public int OrderTotal { get; set; }

        [DisplayName("Order.OrderStatus")]
        public string OrderStatus { get; set; }
        [DisplayName("Order.PaymentStatus")]
        public string PaymentStatus { get; set; }
        [DisplayName("Order.TrackingNumber")]
        public string TrackingNumber { get; set; }
        [DisplayName("Order.Carrier")]
        public string Carrier { get; set; }

        [DisplayName("Order.PaymentDate")]
        public DateTime? PaymentDate { get; set; }
        [DisplayName("Order.PaymentDueDate")]
        public DateTime? PaymentDueDate { get; set; }

        public string SessionId { get; set; }
        [DisplayName("Order.PaymentIntentId")]
        public string PaymentIntentId { get; set; }

        [Required]
        [DisplayName("Order.PhoneNumber")]
        public string PhoneNumber { get; set; }
        [Required]
        [DisplayName("Order.StreetAddress")]
        public string StreetAddress { get; set; }
        //[Required]
        [DisplayName("Order.City")]
        public string City { get; set; }
        //[Required]
        [DisplayName("Order.State")]
        public string State { get; set; }
        [Required]
        [DisplayName("Order.Name")]
        public string Name { get; set; }
        public ICollection<OrderDetails> details { get; set; }
    }
}