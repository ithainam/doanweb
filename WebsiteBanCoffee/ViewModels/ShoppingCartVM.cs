using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteBanCoffee.Models;

namespace WebsiteBanCoffee.Models.ViewModels
{
    public class ShoppingCartVM
    {
        public IEnumerable<ShoppingCart> ListCart { get; set; }

        public Order Order { get; set; }
    }
}