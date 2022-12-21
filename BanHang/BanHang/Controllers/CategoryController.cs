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
        //  Load  danh  sách  sản  phẩm  của  từng  loại  sản  phẩm  (  ví  dụ  loại  sản  phẩm:  điện thoại  thì  sẽ  gồm  các  sản  phẩm  như  iphone  7,..)
        public ActionResult ProductCategory(int Id) 
        {
            var lstProduct=dienThoai.Products.Where(n=>n.CategoryId==Id).ToList();
            return View(lstProduct);
        }
    }
}