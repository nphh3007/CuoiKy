using BanHang.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHang.Controllers
{
    public class OrderController : Controller
    {
        DienThoaiEntities dienThoai = new DienThoaiEntities();
        // GET: Order
        // xem lại đơn hàng mà người dùng đã thanh toán
        public ActionResult Index()
        {
            var lstOrder=dienThoai.OrderDetails.ToList();
            return View(lstOrder);
        }
        public ActionResult Delete(int id)
        {
            var lstOrder=dienThoai.OrderDetails.Where(n=>n.Id==id).FirstOrDefault();
            dienThoai.OrderDetails.Remove(lstOrder);
            dienThoai.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}