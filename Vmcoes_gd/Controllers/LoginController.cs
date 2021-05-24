using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Examination.Controllers;
using Microsoft.AspNetCore.Mvc;
using NurseIndicator.Base.Context;
using Vmcoes_gd.Biz.Users;
using Vmcoes_gd.Context;
using Vmcoes_gd.Dto;
using Vmcoes_gd.Enties;
using Vmcoes_gd.Models;
using Vmcoes_gd.Utils;

namespace Vmcoes_gd.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginDaoService _loginDaoService;
        public LoginController(ILoginDaoService loginDaoService) {
            _loginDaoService = loginDaoService;
        }
      
        public IActionResult Loginout()
        {
            HttpContext.Session.Remove(GlobalConstants.S_USER);
            return RedirectToAction("Logingo", "Login");
        }




        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="userPassword"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Loginsinto(UserVmer vmer) {
            string message = string.Empty;
            JsonCallRes res = null;
            try
            {
                var account = vmer.UserName;
                    var pwd = vmer.newpwd;
                if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(pwd))
                {
                    message = "用户名或密码不能为空.";
                    res = new JsonCallRes(GlobalConstants.ERROR, message, null);
                    return Json(res);
                }
                     vmer.UserName = Base64Util.DecodeBase64(account);
                     vmer.newpwd = Base64Util.DecodeBase64(pwd);
                    User user = _loginDaoService.Use(vmer);//登录验证
                if (user == null) {
                    message = "用户名或密码错误";
                    res = new JsonCallRes(GlobalConstants.ERROR, message, null);
                    return Json(res);
                }
                    HttpContext.Session.Set(GlobalConstants.S_USER, ByteConvertHelper.Object2Bytes(user));
                if (user.Status == 1)
                {
                    res = new JsonCallRes(GlobalConstants.OK, "/Home/Index");
                    return Json(res);
                }
                else if (user.Status == 2)
                {
                    res = new JsonCallRes(GlobalConstants.OK, "/YongHu/YongIndex");
                    return Json(res);
                }
                else
                    return null;


                //return Json(new JsonCallRes(OK, lo));

                //else if (lo.Status == 2)
                //{
                //    return Json(new JsonCallRes(2, lo));
                //}
                //else
                //{
                //    return Json(new JsonCallRes(0,lo));
                //}
            }
            catch (Exception e)
            {
                message = "登录异常";
                res = new JsonCallRes(GlobalConstants.ERROR, message, null);
                return Json(res);
            }

        }
        public IActionResult Logingo()
        {
            return View();
        }

    }
}