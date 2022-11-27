using BanHang.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHang.Areas.Admin.Controllers
{
    public class CategoriesController : Controller
    {
        DienThoaiEntities dienThoai = new DienThoaiEntities();
        // GET: Admin/Categories
        // Load danh sách, tìm kiếm và phân trang các loại sản phẩm
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var lstCategory = new List<Category>();
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
                lstCategory = dienThoai.Categories.Where(n =>
               n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstCategory = dienThoai.Categories.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 5;
            
        int pageNumber = (page ?? 1);
            lstCategory = lstCategory.OrderByDescending(n => n.Id).ToList();
            return View(lstCategory.ToPagedList(pageNumber, pageSize));
        }
        // Chi tiết loại sản phẩm
        public ActionResult Details(int Id)
        {
            var dienThoaiCategory = dienThoai.Categories.Where(n => n.Id == Id).FirstOrDefault();
            return View(dienThoaiCategory);
        }
        // thêm loại sản phẩm
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Category dienThoaiCategory)
        {
            if (ModelState.IsValid)
            {
                try
                {                   
                if (dienThoaiCategory.ImageUpload != null)
                    {
                        string fileName =
                       Path.GetFileNameWithoutExtension(dienThoaiCategory.ImageUpload.FileName);
                        string extension =
                       Path.GetExtension(dienThoaiCategory.ImageUpload.FileName);
                        fileName = fileName + extension;
                        dienThoaiCategory.Avatar = fileName;

                        dienThoaiCategory.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/category"),
                        fileName));
                    }
                    dienThoaiCategory.CreatedOnUtc = DateTime.Now;
                    dienThoai.Categories.Add(dienThoaiCategory);
                    dienThoai.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View(dienThoaiCategory);
        }
        // sửa thông tin loại sản phẩm
        public ActionResult Edit(int id)
        {
            var lstCategory = dienThoai.Categories.Where(n => n.Id == id).FirstOrDefault();
            
        return View(lstCategory);
        }
        [HttpPost]
        public ActionResult Edit(Product category)
        {
            var lstCategory = dienThoai.Categories.Where(n => n.Id ==
           category.Id).FirstOrDefault();
            lstCategory.Name = category.Name;
            lstCategory.Slug = category.Slug;
            lstCategory.DisplayOrder = category.DisplayOrder;
            dienThoai.SaveChanges();
            return RedirectToAction("Index");
        }
        // xóa loại sản phẩm khỏi danh sách
        public ActionResult Delete(int id)
        {
            var lstProduct = dienThoai.Products.Where(n => n.Id == id).FirstOrDefault();
            dienThoai.Products.Remove(lstProduct);
            dienThoai.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

 