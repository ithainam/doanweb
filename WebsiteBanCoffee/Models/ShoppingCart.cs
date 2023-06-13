using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebsiteBanCoffee.Models
{
        public class ShoppingCart
        {
            public int Id { get; set; }

            public int ProductId { get; set; }

            [Range(1, 1000, ErrorMessage = "Nhập vào giá trị giữa 1 đến 1000")]
            [DisplayName("Count")]
            public int Count { get; set; }

            [DisplayName("Price")]
            public int Price { get; set; }

            [DisplayName("SizeOfProduct")]
            public int Size { get; set; }

            [DisplayName("SizeNameOfProduct")]
            public string SizeName { get; set; }

            [DisplayName("SizePriceOfProduct")]
            public int SizePrice { get; set; }

            public string UserId { get; set; }

            [ForeignKey("UserId")]
            public virtual ApplicationUser ApplicationUser { get; set; }

            [ForeignKey("ProductId")]
            public virtual Product Product { get; set; }

            public int IncrementCount(ShoppingCart shoppingCart, int count)
            {
                shoppingCart.Count += count;
                return shoppingCart.Count;
            }

            public int DecrementCount(ShoppingCart shoppingCart, int count)
            {
                shoppingCart.Count -= count;
                return shoppingCart.Count;
            }

        }
}