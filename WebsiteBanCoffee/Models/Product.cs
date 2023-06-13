using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebsiteBanCoffee.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [DisplayName("Mã sản phẩm")]
        public int Id { get; set; }
        [Required]
        [DisplayName("Tên sản phẩm")]
        public string name { get; set; }
        [Required]
        [DisplayName("Mã loại sản phẩm")]
        public int cateID { get; set; }
        [Required]
        [DisplayName("Giá bán ra")]
        public int sellPrice { get; set; }
        [Required]
        [DisplayName("Giá vốn")]
        public int costPrice { get; set; }
        [Required]
        [DisplayName("Mô tả")]
        public string description { get; set; }
        [Required]
        [DisplayName("Hình ảnh")]
        public string image { get; set; }
        [Required]
        [DisplayName("Hiển thị")]
        public bool isDisplay { get; set; }

        [ForeignKey("cateID")]
        public virtual Category cate { get; set; }

        public ICollection<OrderDetails> details { get; set; }

        public static List<Product> GetAllProduct()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            return context.Products.ToList();
        }

        public static List<Product> GetAllProduct(string Cate)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            if (String.IsNullOrEmpty(Cate))
            {
                Cate = context.Categories.FirstOrDefault().name;
            }
            return context.Products.Where(p => p.cate.name.Equals(Cate)).ToList();
        }
    }
}