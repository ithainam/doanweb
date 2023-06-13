using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class OrderDetailDao
    {
        WebShopOnlDbContext db = null;
        public OrderDetailDao()
        {
            db = new WebShopOnlDbContext();
        }
        public bool Insert(OrderDetail detail)
        {
            try
            {
                db.OrderDetails.Add(detail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;

            }
        }
        //public List<OrderDetail> ListOrDetail(long idOrder)
        //{
        //    return db.OrderDetails.Where(x=>x.OrderID==idOrder).OrderByDescending(x => x.ID).ToList();
        //}
    }
}
