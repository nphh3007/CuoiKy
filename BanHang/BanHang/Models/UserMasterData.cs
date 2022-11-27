using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BanHang.Models
{
    public class UserMasterData
    {
        [Required(ErrorMessage = "Vui lòng nhập ")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập ")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập ")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập ")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập ")]
        public string Password { get; set; }
        public Nullable<bool> IsAdmin { get; set; }
        public string Adress { get; set; }
        public Nullable<int> Phone { get; set; }
    }
}