using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MTD.DAL;
using MTD.Helper;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MTD.Models
{
    public class AccountModel: BaseModel
    {
        #region Properties
        // Id người dùng
        public int Id { get; set; }

        // Tên người dùng
        [Required(ErrorMessage = "Xin hãy nhập tên đăng nhập!")]
        // Tên đăng nhập dài từ 6 đến 20 ký tự, bắt đầu bằng chữ cái hoặc số, không được có dấu cách.
        [RegularExpression("^(?=.{6,20}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$", ErrorMessage = "Tên đăng nhập dài từ 6 đến 20 ký tự, bắt đầu bằng chữ cái hoặc số, không được có dấu cách hoặc ký tự đặc biệt")]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        // Email người dùng
        [Required(ErrorMessage = "Xin hãy nhập Email!")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email không đúng định dạng")]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        // Mật khẩu người dùng
        [Required(ErrorMessage = "Xin hãy nhập mật khẩu!")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự, trong đó có ít nhất 01 số.")]
        [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{6,}$", ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự, trong đó có ít nhất 01 số.")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        // Xác nhận lại mật khẩu
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Không khớp với mật khẩu.")]
        public string ConfirmPassword { get; set; }

        // Thời gian đăng ký
        public DateTime Register_Date { get; set; }

        // Mã đăng ký
        public string Active_Code { get; set; }

        // Ngày active.
        public DateTime Active_Date { get; set; }

        // Trạng thái hoạt động
        // 0: Không hoạt động
        // 1: Đang hoạt động
        public short State { get; set; }

        // Trạng thái Xóa
        // 0: Không bị xóa
        // 1: Đã bị xóa
        public bool Del_Flag { get; set; }

        // Quyền người dùng.
        // 1: Thành viên thường
        // 777: Admin
        public int RoleId { get; set; }
        public string RoleText { get; set; }

        public List<SelectListItem> ListRole { get; set; }

        // Lưu tổng số recode khi truy vấn lấy danh sách.
        public int Total { get; set; }

        public List<AccountModel> ListAccount { get; set; }
        #endregion
    }

    public class LoginModel : BaseModel
    {
        [Required(ErrorMessage = "Xin hãy nhập tên đăng nhập hoặc email")]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Xin hãy nhập mật khẩu!")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Remember?")]
        public bool Remember { get; set; }
    }

    public class AccountUpdateModel : BaseModel
    {
        // Id người dùng
        public int Id { get; set; }

        // Tên người dùng
        public string UserName { get; set; }

        // Email người dùng
        public string Email { get; set; }

        // Mật khẩu người dùng
        public string Password { get; set; }

        // Xác nhận lại mật khẩu
        public string ConfirmPassword { get; set; }

        // Trạng thái hoạt động
        // false: Không hoạt động
        // true: Đang hoạt động
        public bool State { get; set; }

        // Trạng thái Xóa
        // 0: Không bị xóa
        // 1: Đã bị xóa
        public bool Del_Flag { get; set; }

        // Quyền người dùng.
        // 1: Thành viên thường
        // 777: Admin
        public int RoleId { get; set; }

        public List<SelectListItem> ListRole { get; set; }

        public void Mapping(AccountUpdateModel AUModel, ref AccountModel model)
        {
            model.Id = AUModel.Id;
            model.UserName = AUModel.UserName;
            model.Email = AUModel.Email;
            model.RoleId = AUModel.RoleId;
            model.State = AUModel.State ? (short)1 : (short)0;
            model.Del_Flag = AUModel.Del_Flag;
        }
    }
}