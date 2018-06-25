using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MTD.Helper;
using MTD.DAL;
using MTD.Models;
using MTD.Services;

namespace MTD.Controllers
{
    public class BaseController : Controller
    {

        //
        // Phân trang.
        public void Paging(int page, int pageSize, int totalRecord)
        {
            ViewBag.page = page;
            ViewBag.pageSize = pageSize;

            ViewBag.MaxPage = (int)Math.Ceiling((double)totalRecord / pageSize);
            ViewBag.PageShow = 5;
            ViewBag.PagePreview = 2;
            ViewBag.TotalRecord = totalRecord;
        }

        /// <summary> Kiểm tra xem tài khoản có phải là Admin hay không.
        /// </summary>
        /// <returns></returns>
        public bool IsAdmin()
        {            
            //int accId = 0; // AccountID
            //int.TryParse(CookieHelper.Get(Configs.COOKIES_ACCOUNT_ID), out accId);
            //AccountService service = new AccountService();
            //return service.IsAdmin(accId);
            try
            {
                if (CookieHelper.Get(Configs.COOKIES_ADMIN) == "1")
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
            
        }

        /// <summary>Lấy thông báo lỗi.
        /// </summary>
        /// <param name="errorKey"></param>
        /// <returns></returns>
        public string GetErrorMessage(int errorKey)
        {
            string result = "";
            switch (errorKey)
            {
                case -1:

                    break;

                case -404:
                    result = Configs.ERROR_NOT_FOUND_ACCOUNT;
                    break;
                default:
                    result = Configs.ERROR_PROCESS;
                    break;
            }
            return result;
        }

        /// <summary>Set template data sử dụng cho toàn web.
        /// </summary>
        /// <param name="message"></param>
        public void SetTempData(string message)
        {
            TempData[Configs.TEMP_MESSAGE] = message;
            TempData[Configs.TEMP_REDIRECT] = "";
        }

        /// <summary> Set template data sử dụng cho toàn web.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="redirect"></param>
        public void SetTempData(string message, string redirect)
        {
            TempData[Configs.TEMP_MESSAGE] = message;
            TempData[Configs.TEMP_REDIRECT] = redirect;
        }

        public void InitAccount(AccountModel model, int dayExpires = 1)
        {
            string isAdmin = model.RoleId == 777 ? "1" : "0";
            
            CookieHelper.Set(Configs.COOKIES_USERNAME, model.UserName, dayExpires);
            CookieHelper.Set(Configs.COOKIES_ADMIN, isAdmin, dayExpires);
            CookieHelper.Set(Configs.COOKIES_ROLE_ID, model.RoleId.ToString(), dayExpires);
            CookieHelper.Set(Configs.COOKIES_ACCOUNT_ID, model.Id.ToString(), dayExpires);
        }

        public void RemoveAccount()
        {
            CookieHelper.Remove(Configs.COOKIES_USERNAME);
            CookieHelper.Remove(Configs.COOKIES_ACCOUNT_ID);
            CookieHelper.Remove(Configs.COOKIES_ADMIN);
            CookieHelper.Remove(Configs.COOKIES_ROLE_ID);
        }
    }
}
