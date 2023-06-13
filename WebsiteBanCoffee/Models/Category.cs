using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebsiteBanCoffee.Models
{
    [Table("Category")]
    public class Category
    {
        [Key]
        [DisplayName("Mã loại sản phẩm")]
        public int Id { get; set; }

        [DisplayName("Tên loại")]
        [Required]
        public string name { get; set; }
        [Required]
        [DisplayName("Size lớn")]
        public bool isLarge { get; set; }
        [Required]
        [DisplayName("Size vừa")]
        public bool isMedium { get; set; }
        [Required]
        [DisplayName("Size nhỏ")]
        public bool isSmall { get; set; }
        [Required]
        [DisplayName("Giá tăng khi upsize")]
        public int bonusPrice { get; set; }
        [Required]
        [DisplayName("Hiển thị")]
        public bool isDisplay { get; set; }
        public ICollection<Product> products { get; set; }
    }
}