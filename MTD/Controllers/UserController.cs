using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MTD.Models;
using MTD.Helper;
using MTD.Services;

namespace MTD.Controllers
{
    public class UserController : BaseController
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }

        #region Account        

        /// <summary>
        /// Đăng ký
        /// </summary>
        /// <returns></returns>
        public ActionResult Registry()
        {
            AccountModel model = new AccountModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Registry(AccountModel model)
        {
            AccountService service = new AccountService();
            if (ModelState.IsValid)
            {
                // Kiểm tra sự tồn tại của username hoặc email đăng ký.
                // Nếu đã tồn tại thì đưa ra thông báo lỗi.
                var chk = service.CheckExistsAccount(model.UserName, model.Email);
                if (chk)
                {
                    model.Message = Configs.ERROR_EXISTS_ACCOUNT;
                    model.Code = -3;
                    model.Result = false;
                    SetTempData(model.Message);
                    return View(model);
                }

                // Cập nhật vào bảng tblAccount.
                string guid = Guid.NewGuid().ToString();
                model.Active_Code = guid;
                model.RoleId = 1;
                int result = service.Insert(model);
                if (result > 0)
                {
                    model.Result = true;
                    model.Message = Configs.SUCCESS_REGISTRY;
                    model.Redirect = Url.Action("Index","Word");
                    SetTempData(model.Message, model.Redirect);
                    return View(model);
                }
                else
                {
                    model.Result = false;
                    model.Message = GetErrorMessage(result);
                    SetTempData(model.Message);
                    return View(model);
                }
            }
            return View(model);
        }

        /// <summary>
        /// Đăng nhập
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            LoginModel model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            AccountService service = new AccountService();
            if (ModelState.IsValid)
            {
                // Kiểm tra sự tồn tại của Username, email.
                bool chk = service.CheckExistsAccount(model.UserName);
                if (chk)
                {
                    // Kiểm tra thông tin đăng nhập.
                    var aId = service.CheckLogin(model.UserName, model.Password);
                    if (aId > 0)
                    {
                        AccountModel accModel = new AccountModel();
                        accModel = service.GetAccountById(aId);

                        // Set Cookie cho tài khoản
                        int dayExpires = model.Remember ? 365 : 1;
                        InitAccount(accModel, dayExpires);

                        // Thông báo đăng nhập thành công & redirect.
                        model.Message = Configs.SUCCESS_LOGIN;
                        model.Redirect = Url.Action("Index", "Word");
                        SetTempData(model.Message, model.Redirect);
                    }
                    else
                    {
                        model.Message = Configs.ERROR_LOGIN;
                        SetTempData(model.Message);
                    }
                }
                else
                {
                    model.Message = Configs.ERROR_NOT_EXISTS_ACCOUNT;
                    SetTempData(model.Message);
                }
            }
            return View(model);
        }

        /// <summary>
        /// Đăng xuất
        /// </summary>
        /// <returns></returns>
        public ActionResult DoLogout()
        {
            RemoveAccount();

            return Redirect("~");
        }

        /// <summary>
        /// Danh sách tài khoản.
        /// Chỉ có Admin mới được xem danh sách này.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ListAccount(AccountCondition conditon)
        {
            AccountService service = new AccountService();
            AccountModel model = new AccountModel();
            model.ListAccount = new List<AccountModel>();
            // Kiểm tra quyền xem danh sách tài khoản.
            if (!IsAdmin())
            {
                model.Message = Configs.ALERT_NOT_ALLOW;
                model.Code = (int)EnumError.ROLE_WRONG;
                model.Result = false;
                return View(model);
            }
            model.ListAccount = service.List(conditon);

            int total = 0;
            if(model.ListAccount.Count>0){
                total = model.ListAccount[0].Total;
            }

            Paging(conditon.page, conditon.pageSize, total);
            
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            AccountModel model = new AccountModel();
            model.ListRole = DefaultValues.ListRole();
            if (!IsAdmin())
            {
                model.Message = Configs.ALERT_NOT_ALLOW;
                model.Code = (int)EnumError.ROLE_WRONG;
                model.Result = false;
                model.Redirect = Url.Action("Index", "Word");
                return View(model);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(AccountModel model)
        {
            model.ListRole = DefaultValues.ListRole();
            if (!IsAdmin())
            {
                model.Message = Configs.ALERT_NOT_ALLOW;
                model.Code = (int)EnumError.ROLE_WRONG;
                model.Result = false;
                model.Redirect = Url.Action("Index", "Word");
                return Redirect("~");
            }
            if (ModelState.IsValid)
            {
                AccountService service = new AccountService();
                // Kiểm tra sự tồn tại của username hoặc email đăng ký.
                // Nếu đã tồn tại thì đưa ra thông báo lỗi.
                var chk = service.CheckExistsAccount(model.UserName, model.Email);
                if (chk)
                {
                    model.Message = Configs.ERROR_EXISTS_ACCOUNT;
                    model.Code = -3;
                    model.Result = false;
                    SetTempData(model.Message);
                    return View(model);
                }

                // Cập nhật vào bảng tblAccount.
                string guid = Guid.NewGuid().ToString();
                model.Active_Code = guid;
                int result = service.Insert(model);
                if (result > 0)
                {
                    model.Result = true;
                    model.Message = Configs.SUCCESS_REGISTRY;
                    model.Redirect = Url.Action("ListAccount", "User");
                    SetTempData(model.Message, model.Redirect);
                    return View(model);
                }
                else
                {
                    model.Result = false;
                    model.Message = GetErrorMessage(result);
                    SetTempData(model.Message);
                    return View(model);
                }
            }
            return View(model);
        }
        
        [HttpGet]
        public ActionResult UpdateAccount(int Id)
        {
            AccountService service = new AccountService();
            AccountUpdateModel model = new AccountUpdateModel();
            model = service.GetAccountUpdateById(Id);
            model.ListRole = DefaultValues.ListRole();

            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateAccount(AccountUpdateModel model)
        {
            model.ListRole = DefaultValues.ListRole();
            if (!IsAdmin())
            {
                model.Message = Configs.ALERT_NOT_ALLOW;
                model.Code = (int)EnumError.ROLE_WRONG;
                model.Result = false;
                model.Redirect = Url.Action("Index", "Word");
                return Redirect("~");
            }
            if (ModelState.IsValid)
            {
                AccountService service = new AccountService();
                AccountModel accModel = new AccountModel();
                model.Mapping(model, ref accModel);
                int result = service.UpdateAccount(accModel);
                if (result > 0)
                {
                    model.Result = true;
                    model.Message = Configs.SUCCESS_UPDATE;
                    model.Redirect = Url.Action("ListAccount", "User");
                    SetTempData(model.Message, model.Redirect);
                    return View(model);
                }
                else
                {
                    model.Result = false;
                    model.Message = GetErrorMessage(result);
                    SetTempData(model.Message);
                    return View(model);
                }
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult DeleteAccount(int Id)
        {
            BaseModel model = new BaseModel();
            if (!IsAdmin())
            {
                Json(-1);
            }
            AccountService service = new AccountService();
            int result = service.UpdateDelFlag(Id);
            return Json(result);
        }

        #endregion

        #region Personal Information
        public ActionResult PersonalInformation(int Id)
        {
            return View();
        }

        #endregion
    }
}
