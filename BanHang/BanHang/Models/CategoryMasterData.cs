using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BanHang.Models
{
    public class CategoryMasterData
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        [Display(Name = "Tên Loại Sản Phẩm")]
        public string Name { get; set; }
        [Display(Name = "Hình Ảnh")]
        public string Avatar { get; set; }
        [Display(Name = "Tên dễ nhớ")]
        public string Slug { get; set; }
        public Nullable<bool> ShowOn { get; set; }
        [Display(Name = "Thứ tự hiển thị")]
        public Nullable<int> DisplayOrder { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public Nullable<System.DateTime> CreatedOnUtc { get; set; }
        public Nullable<System.DateTime> UpdatedOnUtc { get; set; }
    }
}