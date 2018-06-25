using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MTD.DAL;
using MTD.Models;
using MTD.Helper;

namespace MTD.Services
{
    public class AccountService:BaseService
    {
        public AccountService() : base() { }
        public List<AccountModel> List(AccountCondition condition)
        {
            var query = (from e in Context.tblAccounts
                         join r in Context.tblRoles on e.RoleId equals r.Id
                         where (condition.key.Length > 0 ? e.UserName == condition.key || e.Email == condition.key : true)
                         && e.Del_Flag == false
                            select new AccountModel()
                            {
                                Id = e.Id,
                                UserName = e.UserName,
                                Email = e.Email,
                                Password = e.Password,
                                Register_Date = e.Register_Date,
                                Active_Code = e.Active_Code,
                                Active_Date = e.Active_Date,
                                State = e.State,
                                Del_Flag = e.Del_Flag,
                                RoleId = e.RoleId,
                                RoleText = r.Text
                            });
            var result = query.Skip((condition.page - 1) * condition.pageSize).Take(condition.pageSize).ToList();
            if (result != null && result.Count > 0)
            {
                result[0].Total = query.Count();
            }
            return result;
        }

        /// <summary>Thêm 1 tài khoản.
        /// </summary>
        /// <param name="model">Thông tin tài khoản.</param>
        /// <returns></returns>
        public int Insert(AccountModel model)
        {
            try
            {
                // Khởi tạo 1 account lấy các thông tin từ model.
                tblAccount account = new tblAccount()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Password = model.Password,
                    Register_Date = DateTime.Now,
                    Active_Code = model.Active_Code,
                    Active_Date = DateTime.Now,
                    State = 1, // Tạm thời xử lý cho active luôn.
                    Del_Flag = false,
                    RoleId = model.RoleId
                };

                Context.tblAccounts.InsertOnSubmit(account);// Chèn vào bảng tblAccount.
                Context.SubmitChanges();// Cập nhật sự thay đổi của bảng.

                // Trả về ID của tài khoản vừa tạo.
                return account.Id;
            }
            catch (Exception ex)
            {
                string strErr = "Lỗi đăng ký tài khoản: " + model.UserName;
                strErr += "\\n" + ex.ToString();
                Logs.LogWrite(strErr);

                return (int)EnumError.INSERT_ERROR; // Trả về lỗi insert.
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateAccount(AccountModel model)
        {
            try
            {
                tblAccount account = Context.tblAccounts.Where(e => e.Id == model.Id).FirstOrDefault();
                if (account != null)
                {
                    account.State = model.State;
                    account.RoleId = model.RoleId;

                    Context.SubmitChanges();// Cập nhật sự thay đổi của bảng.

                    // Trả về ID của tài khoản.
                    return account.Id;
                }
                else
                {
                    return (int)EnumError.NOT_FOUND;// -404 Not found.
                }
            }
            catch (Exception ex)
            {
                string strErr = "Lỗi cập nhật trạng thái hoạt động Id: " + model.Id;
                strErr += "\\n" + ex.ToString();
                Logs.LogWrite(strErr);
                return (int)EnumError.UPDATE_ERROR;
            }
        }

        /// <summary>Cập nhật trạng thái active (Khi click đường link trong mail)
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Active_Code"></param>
        /// <returns></returns>
        public int UpdateActive(int Id, string Active_Code)
        {
            try
            {
                tblAccount account = Context.tblAccounts.Where(e => e.Id == Id && e.Active_Code == Active_Code).FirstOrDefault();
                if (account != null)
                {
                    account.State = 1;
                    account.Active_Date = DateTime.Now;

                    Context.SubmitChanges();// Cập nhật sự thay đổi của bảng.

                    // Trả về ID của tài khoản.
                    return account.Id;
                }
                else
                {
                    return (int)EnumError.NOT_FOUND;// -404 Not found.
                }
            }
            catch (Exception ex)
            {
                string strErr = "Lỗi cập nhật trạng thái hoạt động Id: " + Id;
                strErr += "\\n" + ex.ToString();
                Logs.LogWrite(strErr);
                return (int)EnumError.UPDATE_ERROR;
            }
        }

        /// <summary> Cập nhật cờ xóa
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int UpdateDelFlag(int Id, bool flg = true)
        {
            try
            {
                tblAccount account = Context.tblAccounts.Where(e => e.Id == Id).FirstOrDefault();
                if (account != null)
                {
                    account.Del_Flag = flg;

                    Context.SubmitChanges();// Cập nhật sự thay đổi của bảng.

                    // Trả về ID của tài khoản.
                    return account.Id;
                }
                else
                {
                    return (int)EnumError.NOT_FOUND;// -404 Not found.
                }
            }
            catch (Exception ex)
            {
                string strErr = "Lỗi cập nhật cờ xóa Id: " + Id;
                strErr += "\\n" + ex.ToString();
                Logs.LogWrite(strErr);
                return -1;
            }
        }

        /// <summary> Cập nhật mật khẩu mới
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="NewPassword"></param>
        /// <returns></returns>
        public int UpdatePassword(int Id, string NewPassword)
        {
            try
            {
                tblAccount account = Context.tblAccounts.Where(e => e.Id == Id).FirstOrDefault();
                if (account != null)
                {
                    account.Password = NewPassword;

                    Context.SubmitChanges();// Cập nhật sự thay đổi của bảng.

                    // Trả về ID của tài khoản.
                    return account.Id;
                }
                else
                {
                    return (int)EnumError.NOT_FOUND;// -404 Not found.
                }
            }
            catch (Exception ex)
            {
                string strErr = "Lỗi cập nhật mật khẩu, Id: " + Id;
                strErr += "\\n" + ex.ToString();
                Logs.LogWrite(strErr);
                return -1;
            }
        }

        /// <summary> Xóa tài khoản
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int Delete(int Id)
        {
            try
            {
                tblAccount account = Context.tblAccounts.Where(e => e.Id == Id).FirstOrDefault();
                if (account != null)
                {
                    Context.tblAccounts.DeleteOnSubmit(account);
                    Context.SubmitChanges();// Cập nhật sự thay đổi của bảng.

                    // Trả về ID của tài khoản.
                    return account.Id;
                }
                else
                {
                    return (int)EnumError.NOT_FOUND;// -404 Not found.
                }
            }
            catch (Exception ex)
            {
                string strErr = "Lỗi xóa tài khoản Id: " + Id;
                strErr += "\\n" + ex.ToString();
                Logs.LogWrite(strErr);
                return -1;
            }
        }

        /// <summary> Kiểm tra tài khoản có phải là Admin hay không.
        /// 
        /// </summary>
        /// <param name="Id">Id tài khoản</param>
        /// <returns></returns>
        public bool IsAdmin(int Id)
        {
            var result = (from a in Context.tblAccounts
                          join r in Context.tblRoles on a.RoleId equals r.Id
                          where a.Id == Id && a.State == 1 && a.Del_Flag == false
                          select r.Code).FirstOrDefault();
            if (result == "Admin")
            {
                return true;
            }
            return false;
        }

        /// <summary>Kiểm tra tài khoản đã được active hay chưa.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool IsActive(int Id)
        {
            var result = (from a in Context.tblAccounts
                          where a.State == 1 && a.Del_Flag == false
                          select a.Id).FirstOrDefault();
            if (result > 0)
            {
                return true;
            }
            return false;
        }

        public bool CheckExistsAccount(string sInput)
        {
            int result = 0;
            if (sInput.Contains('@'))
            {
                result = (from a in Context.tblAccounts
                              where a.Del_Flag == false
                              && (a.Email == sInput)
                              select a.Id).FirstOrDefault();
            }
            else
            {
                result = (from a in Context.tblAccounts
                          where a.Del_Flag == false
                          && (a.UserName == sInput)
                          select a.Id).FirstOrDefault();
            }
            return result > 0;
        }

        /// <summary> Kiểm tra sự tồn tại của tài khoản bằng Username hoặc email
        /// Nếu result > 0 --> đã tồn tại --> return true. 
        /// else --> return false.
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Email"></param>
        /// <returns></returns>
        public bool CheckExistsAccount(string Username, string Email)
        {
            var result = (from a in Context.tblAccounts
                          where a.Del_Flag == false
                          && (a.UserName == Username || a.Email == Email)
                          select a.Id).FirstOrDefault();
            return result > 0;
        }

        /// <summary> Kiểm tra thông tin đăng nhập có chính xác không.
        /// Nếu thông tin đúng --> trả ra Id tài khoản
        /// </summary>
        /// <param name="sInput"></param>
        /// <param name="sPassword"></param>
        /// <returns></returns>
        public int CheckLogin(string sInput, string sPassword)
        {
            var result = (from a in Context.tblAccounts
                          where a.Del_Flag == false
                          && (a.UserName == sInput || a.Email == sInput)
                          && a.Password == sPassword
                          select a.Id).FirstOrDefault();
            return result;
        }


        // Lấy thông tin account theo Id.
        public AccountModel GetAccountById(int aId)
        {
            var result = (from a in Context.tblAccounts
                          where a.Del_Flag == false
                          && a.Id == aId
                          select new AccountModel()
                          {
                              Id = a.Id,
                              UserName = a.UserName,
                              Email = a.Email,
                              Password = a.Password,
                              State = a.State,
                              RoleId = a.RoleId
                          }).FirstOrDefault();
            return result;
        }

        public AccountUpdateModel GetAccountUpdateById(int aId)
        {
            var result = (from a in Context.tblAccounts
                          where a.Del_Flag == false
                          && a.Id == aId
                          select new AccountUpdateModel()
                          {
                              Id = a.Id,
                              UserName = a.UserName,
                              Email = a.Email,
                              Password = a.Password,
                              State = a.State == 1? true: false,
                              RoleId = a.RoleId
                          }).FirstOrDefault();
            return result;
        }
    }
}