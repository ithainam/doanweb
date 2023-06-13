using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteBanCoffee.ViewModels
{
    public class CategoryViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int totalTopping { get; set; }
        public int totalSize { get; set; }

        //  public ToppingCateSizeModel ToppingCateSizeModel { get; set; }

    }
}