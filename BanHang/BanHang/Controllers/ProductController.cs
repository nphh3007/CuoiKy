using BanHang.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHang.Controllers
{
    public class ProductController : Controller
    {
        DienThoaiEntities dienThoai = new DienThoaiEntities();

        // GET: Product
        public ActionResult Index(string search)
        {
            var products = new List<Product>();
            if (!String.IsNullOrEmpty(search))
            {
                products = dienThoai.Products.Where(s => s.Name.Contains(search)).ToList();
            }
            return View(products);
        }
        public ActionResult Detail(int Id)
        {
            var objProduct = dienThoai.Products.Where(n => n.Id == Id).FirstOrDefault();
            return View(objProduct);
        }
    }
}