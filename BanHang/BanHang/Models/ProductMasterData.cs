﻿using BanHang.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BanHang.Models
{
    public partial class ProductMasterData
    {

        public int Id { get; set; }

        [Required(ErrorMessage ="Vui lòng nhập tên sản phẩm")]
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }

        [Display(Name = "Hình ảnh")]
        public string Avatar { get; set; }

        [Display(Name = "Danh mục sản phẩm")]
        public Nullable<int> CategoryId { get; set; }

        [Display(Name = "Mô tả ngắn")]
        public string ShortDes { get; set; }

        [Display(Name = "Mô tả đầy đủ")]
        public string FullDescription { get; set; }

        [Display(Name = "Giá tiền")]
        public Nullable<double> Price { get; set; }

        [Display(Name = "Giá khuyến mãi")]
        public Nullable<double> PriceDiscount { get; set; }

        [Display(Name = "Loại")]
        public Nullable<int> TypeId { get; set; }

        public string Slug { get; set; }

        [Display(Name = "Thương hiệu")]
        public Nullable<int> BrandId { get; set; }

        public Nullable<bool> Deleted { get; set; }
        public Nullable<bool> ShowOnHomePage { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public Nullable<System.DateTime> CreatedOnUtc { get; set; }
        public Nullable<System.DateTime> UpdatedOnUtc { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
        private HttpPostedFileBase imageUpload;
        public HttpPostedFileBase GetImageUpload()
        {
           return imageUpload;
        }
    }
}