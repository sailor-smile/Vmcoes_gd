using NurseIndicator.Base.Context;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Vmcoes_gd.Enties;

namespace Vmcoes_gd.Dto
{
    /// <summary>
    /// 导出工具类
    /// </summary>
    /// <param name="file"></param>
    /// <param name="datalist"></param>
    /// <returns></returns>
    public class ExportScoreSheetUtils
    {
        
        public static string ExportGeneralDeptScore(FileInfo file, PageList<VmcoesGd> datalist)
        {
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet ws = package.Workbook.Worksheets.Add("视频监控详情");
                
                //ws.Cells.Style.ShrinkToFit = true;//单元格自适应
                ws.View.ShowGridLines = false;//去掉sheet的网格线
                //var border = ws.Cells.Style.Border;
                //border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                //List<string> headers = GetHeaders();
                ws.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//水平居中
                ws.Cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;//垂直居中
                ws.Cells.AutoFitColumns();
                ws.Cells.Style.WrapText = true;//自动换行
                //创建标题行
                ws.Cells[1, 1].Value = "普陀区出土工地视频监控检查详情";
                ws.Cells[1, 1].Style.Font.Size = 16;//字体大小
                ws.Cells["A1:J1"].Merge = true;//合并单元格
                ws.Cells["A1:J1"].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);//边框和颜色
                ws.Cells["A1:J1"].Style.Font.Bold = true;//粗体字
                ws.Row(1).Height = 40;//行高

                //创建表头

                ws.Cells[2, 1].Value = "序号";
                ws.Cells[2, 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                ws.Column(1).Width = 5;

                ws.Cells[2, 2].Value = "日期";
                ws.Cells[2, 2].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                ws.Column(2).Width = 14;

                ws.Cells[2, 3].Value = "时间";
                ws.Cells[2, 3].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                ws.Column(3).Width = 14;

                ws.Cells[2, 4].Value = "工地名称";
                ws.Cells[2, 4].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                ws.Column(4).Width = 30;

                ws.Cells[2, 5].Value = "工地地址";
                ws.Cells[2, 5].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                ws.Column(5).Width = 30;

                ws.Cells[2, 6].Value = "运输车辆";
                ws.Cells[2, 6].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                ws.Column(6).Width = 23;

                ws.Cells[2, 7].Value = "是否冲洗";
                ws.Cells[2, 7].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                ws.Column(7).Width = 23;


                ws.Cells[2, 8].Value = "是否密闭";
                ws.Cells[2, 8].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                ws.Column(8).Width = 23;

                ws.Cells[2, 9].Value = "是否办理有效处置证";
                ws.Cells[2, 9].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                ws.Column(9).Width = 20;

                ws.Cells[2, 10].Value = "运输公司名称";
                ws.Cells[2, 10].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                ws.Column(10).Width = 25;

                //合并单元格
                if (null != datalist && datalist.Data.Count >=5)
                {
                    for (int i = 3; i < datalist.Data.Count + 1;i+=5) {
                        ws.Cells[i, 1, i+4, 1].Merge = true;
                        ws.Cells[i, 2, i + 4, 2].Merge = true;
                        ws.Cells[i, 4, i + 4, 4].Merge = true;
                        ws.Cells[i, 5, i + 4, 5].Merge = true;
                    }
                    //ws.Cells[3, 1, 7, 1].Merge = true;
                    //ws.Cells[8, 1, 12, 1].Merge = true;

                    //ws.Cells[3, 3, 7, 3].Merge = true;
                    //ws.Cells[8, 3, 12, 3].Merge = true;

                    //ws.Cells[3, 4, 7, 4].Merge = true;
                    //ws.Cells[8, 4, 12, 4].Merge = true;
                }



                int[] scores = {1,1,1,1,1,2,2,2,2,2,3,3,3,3,3,4,4,4,4,4,5,5,5,5,5,6,6,6,6,6,7,7,7,7,7,8,8,8,8,8,9,9,9,9,9,10,10,10,10,10};
                //添加内容
                if (null != datalist && datalist.Data.Count != 0)
                {
                    for (int i = 1; i < datalist.Data.Count + 1; i++)
                    {
                        var data = datalist.Data[i - 1];
                        var Rinse = data.Rinse.ToString();
                        var Airtight = data.Airtight.ToString();
                        var Certificate = data.Certificate.ToString();
                        if (data.Rinse.ToString() == "0")
                        { Rinse = "是"; }
                        else
                        { Rinse = "否"; }

                        if (data.Airtight.ToString() == "0")
                        { Airtight = "是"; } 
                        else 
                        { Airtight = "否"; }

                        if (data.Certificate.ToString() == "0")
                        { Certificate = "是"; }
                        else
                        { Certificate = "否"; }
                        string[] values = {
                                     scores[i-1].ToString() ,
                                    data.InputTime.Value.ToString("d"),
                                    data.InputTime.Value.ToString("t"),
                                    data.ConstName.ToString(),
                                    data.ConstAddr.ToString(),
                                     data.Trans.ToString(),
                                    Rinse,
                                    Airtight,
                                    Certificate,
                                    data.TransportcompanyName.ToString()
                                };
                        for (int k = 1; k < values.Length + 1; k++)
                        {
                            ws.Cells[2+ i, k].Value = values[k - 1];
                            ws.Cells[2 + i, k].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                        }
                      
                        ws.Column(i).Width = 15;
                    
                    }
                }
                
                package.Save();
            }
            return null;
        }


        //    //获取表头
        //    public static List<string> GetHeaders()
        //    {
        //        List<string> headers = new List<string>
        //{
        //    "学号",
        //    "姓名",
        //    "就诊医院",
        //    "就诊科室",
        //    "发票号码",
        //    "发票日期",
        //    "发票金额",
        //    "报销级别",
        //    "报销比例%",
        //    "可报金额",
        //    "实报金额",
        //    "审核日期",
        //    "报销日期",
        //    "发票状态"
        //};
        //        return headers;
        //    }

        //public static List<string> GetColumns(DataTable scoreTable)
        //{
        //    int count = scoreTable.Columns.Count;
        //    List<string> columns = new List<string>(count);
        //    string colName = null;
        //    for (int i = 0; i < count; i++)
        //    {
        //        colName = scoreTable.Columns[i].ColumnName;
        //        columns.Add(colName);
        //    }
        //    return columns;
        //}

    }
        } 
