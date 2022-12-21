using BanHang.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHang.Areas.Admin.Controllers
{
    public class BrandsController : Controller
    {
        DienThoaiEntities dienThoai = new DienThoaiEntities();
        // GET: Admin/Brands
        // Load danh sách, tìm kiếm và phân trang các thương hiệu có trong db
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var lstBrand = new List<Brand>();
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
                lstBrand = dienThoai.Brands.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstBrand = dienThoai.Brands.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            lstBrand = lstBrand.OrderByDescending(n => n.Id).ToList();
            return View(lstBrand.ToPagedList(pageNumber, pageSize));
        }
        //chi tiết thương hiệu
        public ActionResult Details(int Id)
        {
            var dienThoaiBrand = dienThoai.Brands.Where(n => n.Id == Id).FirstOrDefault();
            return View(dienThoaiBrand);
        }
        //thêm thương hiệu
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Brand dienThoaiBrand)
        {
            dienThoaiBrand.CreatedOnUtc = DateTime.Now;
            dienThoai.Brands.Add(dienThoaiBrand);
            dienThoai.SaveChanges();
            return RedirectToAction("Index");
        }
        //sửa thương hiệu
        public ActionResult Edit(int id)
        {
            var lstBrand = dienThoai.Brands.Where(n => n.Id == id).FirstOrDefault();
            return View(lstBrand);
        }
        [HttpPost]
        public ActionResult Edit(Brand brand)
        {
            var lstBrand = dienThoai.Brands.Where(n => n.Id == brand.Id).FirstOrDefault();
            lstBrand.Name = brand.Name;
            lstBrand.Slug = brand.Slug;
            lstBrand.DisplayOnOrder = brand.DisplayOnOrder;

            dienThoai.SaveChanges();
            return RedirectToAction("Index");
        }
        // xóa thương hiệu
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objBrand = dienThoai.Brands.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }
        [HttpPost]
        public ActionResult Delete(Brand objBr)
        {
            var objBrand = dienThoai.Brands.Where(n => n.Id == objBr.Id).FirstOrDefault();
            dienThoai.Brands.Remove(objBrand);
            dienThoai.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}