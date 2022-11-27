using BanHang.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHang.Controllers
{
    public class CategoryController : Controller
    {
        DienThoaiEntities dienThoai = new DienThoaiEntities();

        // GET: Category
        public ActionResult Index()
        {
            var lstCategory=dienThoai.Categories.ToList();
            return View(lstCategory);
        }
        public ActionResult ProductCategory(int Id) 
        {
            var lstProduct=dienThoai.Products.Where(n=>n.CategoryId==Id).ToList();
            return View(lstProduct);
        }
    }
}