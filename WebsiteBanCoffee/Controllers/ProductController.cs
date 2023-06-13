using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanCoffee.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Migrations;
using System.Data.Entity;
using System.Xml.Linq;
using X.PagedList;


namespace WebsiteBanCoffee.Controllers
{
    
    
    public class ProductController : Controller
    {
        private ApplicationDbContext context;
        public ProductController()
        {
            context = new ApplicationDbContext();
        }
        // GET: Product
        public ActionResult Index(string name, double? to, double? from,int? id)
        {
            
            var listProducts = from b in context.Products.Where(b=>b.isDisplay) select b;
            if (!string.IsNullOrEmpty(name))
            {
                if (to != null && from != null)
                {
                    listProducts = listProducts.Where(x => x.name.Contains(name) && x.sellPrice >= to && x.sellPrice <= from);
                }
                else
                {
                    listProducts = listProducts.Where(x => x.name.Contains(name));
                }
            }
            else if (to != null && from != null)
            {
                listProducts = listProducts.Where(x => x.name.Contains(name) && x.sellPrice >= to && x.sellPrice <= from);
            }

            else if (id!=null&& to==null&&from==null)
            {
                listProducts = listProducts.Where(x => x.name.Contains(name) && x.cateID==id);
            }                  
            return View(listProducts);
        }
        //public ActionResult phantrang(int? page)
        //{
        //    page = page <1 ? 1:page;
        //    int pageSize = 3;
        //    object pr = context.PagedList(page, pageSize);
        //    return View(pr);
        //}



        public ActionResult Details(int productId)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            Product prod = context.Products.Include(p => p.cate).FirstOrDefault(p => p.Id == productId && p.isDisplay);
            ShoppingCart cartObj = new ShoppingCart();
            cartObj.Count = 1;
            cartObj.ProductId = productId;
            cartObj.Product = prod;

            var listSize = context.Categories.FirstOrDefault(p => p.Id == prod.cateID);
            //ViewBag.ListSizeLength = listSize.Count();

            if (prod.cate.isSmall == true)
            {
                if (prod.cate.isMedium == true)
                {
                    if (prod.cate.isLarge == true)
                    {
                        ViewBag.PriceSmall = 0;
                        ViewBag.PriceMedium = listSize.bonusPrice;
                        ViewBag.PriceLarge = listSize.bonusPrice * 2;
                    }
                    else
                    {
                        ViewBag.PriceSmall = 0;
                        ViewBag.PriceMedium = listSize.bonusPrice;
                        ViewBag.PriceLarge = 0;
                    }
                }
                else
                {
                    if (prod.cate.isLarge == true)
                    {
                        ViewBag.PriceSmall = 0;
                        ViewBag.PriceMedium = 0;
                        ViewBag.PriceLarge = listSize.bonusPrice;
                    }
                    else
                    {
                        ViewBag.PriceSmall = 0;
                        ViewBag.PriceMedium = 0;
                        ViewBag.PriceLarge = 0;
                    }
                }
            }
            else if (prod.cate.isMedium == true)
            {
                if (prod.cate.isSmall == true)
                {
                    if (prod.cate.isLarge == true)
                    {
                        ViewBag.PriceSmall = 0;
                        ViewBag.PriceMedium = listSize.bonusPrice;
                        ViewBag.PriceLarge = listSize.bonusPrice * 2;
                    }
                    else
                    {
                        ViewBag.PriceSmall = 0;
                        ViewBag.PriceMedium = listSize.bonusPrice;
                        ViewBag.PriceLarge = 0;
                    }
                }
                else
                {
                    if (prod.cate.isLarge == true)
                    {
                        ViewBag.PriceSmall = 0;
                        ViewBag.PriceMedium = 0;
                        ViewBag.PriceLarge = listSize.bonusPrice;
                    }
                    else
                    {
                        ViewBag.PriceSmall = 0;
                        ViewBag.PriceMedium = 0;
                        ViewBag.PriceLarge = 0;
                    }
                }
            }
            else if (prod.cate.isLarge == true)
            {
                if (prod.cate.isSmall == true)
                {
                    if (prod.cate.isMedium == true)
                    {
                        ViewBag.PriceSmall = 0;
                        ViewBag.PriceMedium = listSize.bonusPrice;
                        ViewBag.PriceLarge = listSize.bonusPrice * 2;
                    }
                    else
                    {
                        ViewBag.PriceSmall = 0;
                        ViewBag.PriceMedium = 0;
                        ViewBag.PriceLarge = listSize.bonusPrice;
                    }
                }
                else
                {
                    if (prod.cate.isMedium == true)
                    {
                        ViewBag.PriceSmall = 0;
                        ViewBag.PriceMedium = 0;
                        ViewBag.PriceLarge = listSize.bonusPrice;
                    }
                    else
                    {
                        ViewBag.PriceSmall = 0;
                        ViewBag.PriceMedium = 0;
                        ViewBag.PriceLarge = 0;
                    }
                }
            }
            else
            {
                ViewBag.PriceSmall = 0;
                ViewBag.PriceMedium = 0;
                ViewBag.PriceLarge = 0;
            }

            ViewBag.ListSize = listSize;
            //ViewBag.PriceSmall = listSize.bonusPrice;
            //ViewBag.PriceMedium = listSize.bonusPrice * 2;
            //ViewBag.PriceLarge = listSize.bonusPrice * 3;

            Session["CountCart"] = context.ShoppingCarts.Count();

            return View(cartObj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Details(ShoppingCart shoppingCart, int SizeOfProduct, int SizePriceOfProduct)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            ApplicationUser loginUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            if (shoppingCart.Count <= 0)
            {
                return RedirectToAction("Index", "Product");
            }

            string userId = loginUser.Id;

            if (SizeOfProduct == null)
            {
                SizeOfProduct = 0;
            }

            shoppingCart.Size = SizeOfProduct;

            //if (shoppingCart.Size == null)
            //{
            //    shoppingCart.SizePrice = 0;
            //} else
            //{
            shoppingCart.SizePrice = SizePriceOfProduct;
            //}


            string nameSize = "";

            if (SizeOfProduct == 2)
            {
                nameSize = "Medium";
            }
            else if (SizeOfProduct == 3)
            {
                nameSize = "Large";
            }
            else if (SizeOfProduct == 1)
            {
                nameSize = "Small";
            }
            else
            {
                nameSize = "Special";
            }

            shoppingCart.SizeName = nameSize;

            shoppingCart.UserId = userId;

            ShoppingCart cartFromDb = context.ShoppingCarts.FirstOrDefault(u => u.UserId == userId && u.ProductId == shoppingCart.ProductId && u.Size == SizeOfProduct);

            var findProduct = context.Products.Include(p => p.cate).FirstOrDefault(p => p.Id == shoppingCart.ProductId);

            shoppingCart.Price = (findProduct.sellPrice + shoppingCart.SizePrice) * shoppingCart.Count;

            if (cartFromDb == null)
            {
                context.ShoppingCarts.AddOrUpdate(shoppingCart);
                context.SaveChanges();
            }
            else
            {
                cartFromDb.Count = cartFromDb.Count + shoppingCart.Count;
                cartFromDb.Price = cartFromDb.Price + shoppingCart.Price;
                context.SaveChanges();
            }


            Session["CountCart"] = context.ShoppingCarts.Count();

            return RedirectToAction(nameof(Index));
        }


    }
}