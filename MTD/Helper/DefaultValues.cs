using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MTD.Helper
{
    // Lưu các giá trị mặc định
    public static class DefaultValues
    {
        public static List<SelectListItem> ListRole()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text = "Thành viên thường", Value = "1" });
            list.Add(new SelectListItem() { Text = "Quản lý", Value = "777" });
            return list;
        }
    }
}