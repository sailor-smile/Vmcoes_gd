
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Vmcoes_gd.Context;
using Vmcoes_gd.Enties;
using Vmcoes_gd.Utils;

namespace Examination.Controllers
{
    /// <summary>
    /// 控制器抽象
    /// </summary>
    public class BaseController : Controller
    {
        protected static int ERROR = 0;
        protected static int OK = 1;
     

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            byte[] result;
            HttpContext.Session.TryGetValue(GlobalConstants.S_USER, out result);
            if (result == null)
            {
                filterContext.Result = new RedirectResult("/Login/Logingo");
                return;
            }
            else
            {
                User user = ByteConvertHelper.Bytes2Object<User>(result);
                base.ViewData["realName"] = user.ActualName;
                base.ViewData["uid"] = user.UId;
                base.ViewData["wd"] = user.UPassword;

            }
            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// 获取当前登录用户信息
        /// </summary>
        /// <returns></returns>
        public User GetUser()
        {
            HttpContext.Session.TryGetValue(GlobalConstants.S_USER, out byte[] userBytes);
            return ByteConvertHelper.Bytes2Object<User>(userBytes);
        }
    }
}