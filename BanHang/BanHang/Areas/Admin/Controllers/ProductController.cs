using BanHang.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static BanHang.Common;

namespace BanHang.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        DienThoaiEntities dienThoai = new DienThoaiEntities();
        // GET: Admin/Product
        public ActionResult Index(string SeachString,string currentFilter, int? page)
        {

            var lstProduct = new List<Product>();
            if (SeachString != null)
            {
                page= 1;
            }
            else
            {
                SeachString = currentFilter;
            }
            if(!string.IsNullOrEmpty(SeachString))
            {
                //lấy ds sản phẩm theo từ khóa
                lstProduct=dienThoai.Products.Where(n=>n.Name.Contains(SeachString)).ToList();
            }
            else
            {
                //lấy all sản phẩm trong bản product
                lstProduct=dienThoai.Products.ToList();
            }
            ViewBag.CurrentFilter = SeachString;
            //số lượng item của 1 trang =4
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            //sắp xếp theo id sản phẩm, sp mới đưa lên đầu
            lstProduct=lstProduct.OrderByDescending(n=>n.Id).ToList();
            return View(lstProduct.ToPagedList(pageNumber,pageSize));
        }


        [HttpGet]
        public ActionResult Create() 
        {
            loaddata();
            return View(); ;
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Product objProduct)
        {
            loaddata(); 
            if (ModelState.IsValid)
            {
                try
                {

                    if (objProduct.ImageUpload != null)
                    {
                        {
                            string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                            string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                            fileName = fileName + extension;
                            objProduct.Avatar = fileName;
                            objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items"), fileName));

                        }
                        objProduct.CreatedOnUtc = DateTime.Now;
                        dienThoai.Products.Add(objProduct);
                        dienThoai.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(objProduct);
        }

        public ActionResult Details(int id)
        {
            var objProduct=dienThoai.Products.Where(n=>n.Id==id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objProduct = dienThoai.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpPost]
        public ActionResult Delete(Product objPr)
        {
            var objProduct = dienThoai.Products.Where(n => n.Id == objPr.Id).FirstOrDefault();
            dienThoai.Products.Remove(objProduct);  
            dienThoai.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objProduct = dienThoai.Products.Where(n => n.Id == id).FirstOrDefault();
            objProduct.UpdatedOnUtc = DateTime.Now;
            return View(objProduct);
        }
        [HttpPost]
        public ActionResult Edit(int id,Product objProduct)
        {
            if (objProduct.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                fileName = fileName + extension;
                objProduct.Avatar = fileName;
                objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items"), fileName));

            }
            dienThoai.Entry(objProduct).State=System.Data.Entity.EntityState.Modified; 
            dienThoai.SaveChanges();
            return RedirectToAction("Index");
        }
        void loaddata()
        {
            Common objCommon = new Common();
        var lstCat = dienThoai.Categories.ToList();
        ListtoDataTableConverter converter = new ListtoDataTableConverter();
        DataTable dtCategory = converter.ToDataTable(lstCat);
        ViewBag.ListCategory =objCommon.ToSelectList(dtCategory, "Id", "Name");

            var lstBrand = dienThoai.Brands.ToList();
        DataTable dtBrand = converter.ToDataTable(lstBrand);
        ViewBag.ListBrand = objCommon.ToSelectList(dtBrand, "Id", "Name");

            List<ProductType> lstProductType = new List<ProductType>();
        ProductType objProductType = new ProductType();
        objProductType.Id = 1;
            objProductType.Name = "Giảm giá sốc";
            lstProductType.Add(objProductType);

            objProductType = new ProductType();
        objProductType.Id = 2;
            objProductType.Name = "Đề xuất";
            lstProductType.Add(objProductType);

            DataTable dtProductType = converter.ToDataTable(lstProductType);
        ViewBag.ListProductType = objCommon.ToSelectList(dtProductType, "Id", "Name");

        }
    }
}