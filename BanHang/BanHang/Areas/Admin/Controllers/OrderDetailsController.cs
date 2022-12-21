using BanHang.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHang.Areas.Admin.Controllers
{
    public class OrderDetailsController : Controller
    {
        DienThoaiEntities dienThoai = new DienThoaiEntities();

        // GET: Admin/OrderDetails
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var lstOrder = new List<OrderDetail>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                lstOrder = dienThoai.OrderDetails.Where(n =>
               n.Order.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstOrder = dienThoai.OrderDetails.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            lstOrder = lstOrder.OrderByDescending(n => n.Id).ToList();
            return View(lstOrder.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Details(int Id)
        {
            var dienThoaiOrder = dienThoai.OrderDetails.Where(n => n.Id == Id).FirstOrDefault();
            return View(dienThoaiOrder);
        }
        public ActionResult Delete(int id)
        {
            var lstOrder = dienThoai.OrderDetails.Where(n => n.Id == id).FirstOrDefault();
            dienThoai.OrderDetails.Remove(lstOrder);
            dienThoai.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}