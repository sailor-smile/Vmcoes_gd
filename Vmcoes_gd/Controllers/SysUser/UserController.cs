using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Examination.Controllers;
using Microsoft.AspNetCore.Mvc;
using NurseIndicator.Base.Context;
using Vmcoes_gd.Biz.Users;

namespace Vmcoes_gd.Controllers.SysUser
{
    public class UserController : BaseController
    {
        private readonly ILoginDaoService  _loginDaoService;
        public UserController(ILoginDaoService loginDaoService)
        {
            _loginDaoService = loginDaoService;
        }
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="updateAll"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UpdatePwd(string newpwd)
        {
            try
            {
                var update = _loginDaoService.updatePwd(GetUser().UId, newpwd);
                return Json(new JsonCallRes(update));
            }
            catch (Exception e)
            {

                return Json(new JsonCallRes(ERROR));
            }
        }
    }
}