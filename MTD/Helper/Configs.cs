using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MTD.Helper
{
    public class Configs
    {
        // Cookie 
        public const string COOKIES_USERNAME = "UserName";
        public const string COOKIES_ACCOUNT_ID = "AccountID";
        public const string COOKIES_ROLE_ID = "RoleID";
        public const string COOKIES_ADMIN = "Admin";

        public const string TEMP_MESSAGE = "MESSAGE";
        public const string TEMP_REDIRECT = "REDIRECT";
        
        // Alert
        public const string ALERT_NOT_ALLOW = "Bạn không được phép truy cập chức năng này.";
        public const string ALERT_NOT_CHOICE = "Bạn chưa chọn đối tượng nào.";

        // Confirm
        public const string CONFIRM_DELETE_USERS = "Bạn có chắc chắn muốn xóa tài khoản này không?";
        public const string CONFIRM_LOGOUT ="Bạn có muốn thoát không?";

        // Error
        public const string ERROR_DELETE = "Xóa không thành công";
        public const string ERROR_NOT_DATA = "Không có dữ liệu";
        public const string ERROR_EXISTS_ACCOUNT = "Tài khoản bạn chọn đã tồn tại. Xin xem lại Username hoặc Email.";
        public const string ERROR_NOT_EXISTS_ACCOUNT = "Tài khoản username hoặc email chưa tồn tại";
        public const string ERROR_PROCESS = "Lỗi xử lý dữ liệu";
        public const string ERROR_NOT_FOUND_ACCOUNT = "Không tìm thấy tài khoản phù hợp.";
        public const string ERROR_LOGIN = "Thông tin đăng nhập không khớp. Xin vui lòng xem lại.";

        // Success
        public const string SUCCESS_REGISTRY = "Xin chúc mừng! bạn đã đăng ký thành công.";
        public const string SUCCESS_INSERT = "Bạn đã thêm thành công";
        public const string SUCCESS_UPDATE = "Bạn đã cập nhật thành công";
        public const string SUCCESS_DELETE = "Xóa thành công";
        public const string SUCCESS_LOGIN = "Xin chúc mừng! Bạn đã đăng nhập thành công.";

        // Message

    }
}