using BanHang.Context;
using BanHang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHang.Controllers
{
    public class PaymentController : Controller
    {
        DienThoaiEntities dienThoai = new DienThoaiEntities();
        // GET: Payment
        public ActionResult Index()
        {
            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                //Lấy thông tin từ giỏ hàng từ biển session
                var lstCart = (List<CartModel>)Session["cart"];
                //gán dữ liệu cho Order
                Order dtOrder = new Order();
                dtOrder.Name = "DonHang-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                dtOrder.UserId = int.Parse(Session["idUser"].ToString());
                dtOrder.CreateOnUtc= DateTime.Now;
                dtOrder.Status = 1;                
                dienThoai.Orders.Add(dtOrder);
                //Lưu thông tin dữ liệu vào bảng order
                dienThoai.SaveChanges();
                //Lấy order vừa mới tạo để lưu vào bảng orderdetail
                int intOrderId = dtOrder.Id;
                List<OrderDetail> lstOrderDetails = new List<OrderDetail>();
                foreach (var item in lstCart)
                {
                    OrderDetail obj = new OrderDetail();
                    obj.Quantity = item.Quantity;
                    obj.OrderId = intOrderId;
                    obj.ProductId = item.Product.Id;
                    lstOrderDetails.Add(obj);
                }
                dienThoai.OrderDetails.AddRange(lstOrderDetails);
                dienThoai.SaveChanges();
            }
                return View();
        }
    }
}