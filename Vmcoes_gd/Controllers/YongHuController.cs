using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Examination.Controllers;
using Microsoft.AspNetCore.Mvc;
using NurseIndicator.Base.Context;
using Vmcoes_gd.Biz.Car;
using Vmcoes_gd.Dto;

namespace Vmcoes_gd.Controllers
{
    public class YongHuController : BaseController

    {
        //注入
        private readonly IVmcoesgdService _vmcoesgdService;
        public YongHuController(IVmcoesgdService vmcoesgdService)
        {
            _vmcoesgdService = vmcoesgdService;
        }

        public IActionResult YongIndex() {
            return View();
        }

       



    }
}