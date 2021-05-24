using Examination.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NurseIndicator.Base.Context;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Drawing;
using System.IO;
using Vmcoes_gd.Biz.Car;
using Vmcoes_gd.Biz.Users;
using Vmcoes_gd.Dto;
using Vmcoes_gd.Enties;

namespace Vmcoes_gd.Controllers
{
    public class HomeController : BaseController
    {
        //注入
        private const string EXP_FILE_PREFIX = "\\fiile\\ins\\";
        private readonly IHostingEnvironment hostEnv;
        private readonly IVmcoesgdService _vmcoesgdService;
        private readonly ILoginDaoService _loginDaoService;
        public HomeController(IVmcoesgdService vmcoesgdService, IHostingEnvironment _hostEnv,ILoginDaoService loginDaoService)
        {
            hostEnv = _hostEnv;
            _vmcoesgdService = vmcoesgdService;
            _loginDaoService = loginDaoService;
        }
        //某个页面跳转传值给跳转的页面
        public IActionResult Modify(string d) {
            ViewData["dataId"] = d;
            
            return View();
        }
        public IActionResult Index(string u)
        {
            ViewData["defDate"] = u;
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }


 


        /// <summary>
        /// 修改多条
        /// </summary>
        /// <param name="vmcoesGd"></param>
        /// <returns></returns>
        public IActionResult Updateall(UpdateAll  updateAll)
        {
            try
            {
                var update = _vmcoesgdService.Updateall(updateAll);
                return Json(new JsonCallRes(update));
            }
            catch (Exception e)
            {

                return Json(new JsonCallRes(ERROR));
            }
        }



        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        public IActionResult Update(VmcoesGd vmcoesGd)
        {
            try
            {
                var update = _vmcoesgdService.Update(vmcoesGd);
                return Json(new JsonCallRes(update));
            }
            catch (Exception e)
            {

                return Json(new JsonCallRes(ERROR));
            }
        }


        /// <summary>
        ///  获取详情
        /// </summary>
        /// <param name="dataId">ID</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetVmcogd(int dataId)
        {
            try
            {
                var data = _vmcoesgdService.GetVmcoes(dataId);
                return Json(new JsonCallRes(OK, data));
            }
            catch (Exception e)
            {
                return Json(new JsonCallRes(ERROR));
            }
        }


        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageCond"></param>
        /// <returns></returns>

        [HttpPost]
        public IActionResult FindforPage(PageCond<Vmparameter> pageCond)
        {
            try
            {
                var list = _vmcoesgdService.FindVmcoes(pageCond);
                return Json(new JsonCallRes(OK, list));
            }
            catch (Exception e)
            {
                return Json(new JsonCallRes(ERROR));
            }

        }




        /// <summary>
        ///导出
        /// </summary>
        /// <param name="dto">查询条件</param>
        /// <returns></returns>
        public IActionResult ExportExcel(DateTime? gdrq)
        {
            try
            {
                Vmparameter cond = new Vmparameter
                {
                    gdrq = gdrq
                };
                string webBootPath = GetTuple().Item1;
                string fileName = GetTuple().Item2;
                var tempFile = Path.Combine(webBootPath + EXP_FILE_PREFIX, fileName);
                FileInfo file = new FileInfo(tempFile);
                var list = _vmcoesgdService.FindVmcoesGdorTime(cond);
                ExportScoreSheetUtils.ExportGeneralDeptScore(file, list);
                return File(EXP_FILE_PREFIX + fileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            catch (Exception e)
            {
                
                return Json(new JsonCallRes(ERROR));
            }
        }
        private Tuple<string, string> GetTuple()
        {
            string sWebRootFolder = hostEnv.WebRootPath;
            string sFileName = $"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            return Tuple.Create(sWebRootFolder, sFileName);
        }

        ///// <summary>
        ///// 根据时间区间查询
        ///// </summary>
        ///// <param name="pageCond"></param>
        ///// <returns></returns>

        //[HttpPost]
        //public IActionResult FindVmcoesTimes(Vmparameter vmparameter)
        //{
        //    try
        //    {
        //        if(vmparameter.StartTime<=vmparameter.EndTime){
        //            var list = _vmcoesgdService.FindVmcoesTime(vmparameter);
        //            return Json(new JsonCallRes(OK, list));
        //        }else{
        //            return Json(new JsonCallRes(ERROR));
        //        }
                
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(new JsonCallRes(ERROR));
        //    }

        //}


        /// <summary>
        /// 根据日期查询
        /// </summary>
        /// <param name="pageCond"></param>
        /// <returns></returns>

        [HttpPost]
        public IActionResult FindVmcoesGdorTimes(Vmparameter vmparameter)
        {
            try
            {
              
                   var list = _vmcoesgdService.FindVmcoesGdorTime(vmparameter);
                    return Json(new JsonCallRes(OK, list));
              
                

            }
            catch (Exception e)
            {
                return Json(new JsonCallRes(ERROR));
            }

        }



        /// <summary>
        /// 当前时间
        /// </summary>
        /// <param name="pageCond"></param>
        /// <returns></returns>

        [HttpPost]
        public IActionResult FindVmcoesGdorNowadaysTime()
        {
            try
            {
                    var list = _vmcoesgdService.FindVmcoesGdorNowadaysTime();
                
                    return Json(new JsonCallRes(OK, list));
            }
            catch (Exception e)
            {
                return Json(new JsonCallRes(ERROR));
            }

        }








        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="vmcoesGd"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Save(SaveAll cond) {
            try
            {
               
                var state = _vmcoesgdService.Save(cond);
                return Json(new JsonCallRes(state));
            }
            catch (Exception e)
            {

                return Json(new JsonCallRes(ERROR));
            }
        }


        /// <summary>
        /// 动态获取下拉工地名称
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult fingd() {
            try
            {
                var fingd = _vmcoesgdService.Site();
                //var o = new ConstructionSite();
                //o.ConstName = "wokao";
                return Json(new JsonCallRes(OK,fingd));

            }
            catch (Exception e)
            {
                return Json(new JsonCallRes(ERROR, null));
            }
        }

        [HttpPost]
        public IActionResult Delete(int id) {
            try
            {
                _vmcoesgdService.Delet(id);
                return Json(new JsonCallRes(OK));
                

            }
            catch (Exception e)
            {
                return Json(new JsonCallRes(ERROR));

            }


        }






    }
}
