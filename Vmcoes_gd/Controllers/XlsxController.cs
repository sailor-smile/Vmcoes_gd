using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace Vmcoes_gd.Controllers
{
    public class XlsxController : Controller
    {
        //用来获取路径相关
        private IHostingEnvironment _hostingEnvironment;

        public XlsxController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Export()
        {
            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = "记录.xlsx";

            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            file.Delete();
            using (ExcelPackage package = new ExcelPackage(file))
            {
                // 添加worksheet
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("记录");
                //添加头
                worksheet.Cells[1, 1].Value = "序号";
                worksheet.Cells[1, 2].Value = "日期";
                worksheet.Cells[1, 3].Value = "时间";
                worksheet.Cells[1, 4].Value = "工地名称";
                worksheet.Cells[1, 5].Value = "工地地址";
                worksheet.Cells[1, 6].Value = "运输车辆";
                worksheet.Cells[1, 7].Value = "是否冲洗";
                worksheet.Cells[1, 8].Value = "是否密闭";
                worksheet.Cells[1, 9].Value = "是否办理有效处置证";
                worksheet.Cells[1, 10].Value = "运输公司名称";
                //添加值
                worksheet.Cells["A2"].Value = 1000;
                worksheet.Cells["B2"].Value = "For丨丶";
                worksheet.Cells["C2"].Value = "https://buluo.qq.com/p/barindex.html?bid=310072";
                worksheet.Cells["D2"].Value = 1000;
                worksheet.Cells["E2"].Value = "For丨丶";
                worksheet.Cells["F2"].Value = "https://buluo.qq.com/p/barindex.html?bid=310072";
                worksheet.Cells["G2"].Value = 1000;
                worksheet.Cells["H2"].Value = "For丨丶";
                worksheet.Cells["I2"].Value = "https://buluo.qq.com/p/barindex.html?bid=310072";
                worksheet.Cells["J2"].Value = 1000;

                package.Save();
            }
            return File(sFileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);
        }




    }
}