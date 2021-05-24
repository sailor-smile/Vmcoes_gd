using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vmcoes_gd.Enties
{
    public partial class VmcoesGd
    {

        public int Id { get; set; }
        public DateTime? InputRiqi { get; set; }//月份
        public DateTime? InputTime { get; set; }//时间(13:48)
        public string ConstCode { get; set; }//工地号
        public string ConstName { get; set; }//工地名称
        public string ConstAddr { get; set; }//工地地址
        public string Trans { get; set; }//运输车辆
        public string Rinse { get; set; }//是否冲洗
        public string Airtight { get; set; }//是否密闭
        public string Certificate { get; set; }//是否办理有效处置证
        public string TransportcompanyName { get; set; }//运输公司名称
        public short? Status { get; set; }//状态
        public int? SortNo { get; set; }//排序

        [NotMapped]
        public string Date { get; set; }//日期
        [NotMapped]
        public string Datein { get; set; }//无年份日期
        [NotMapped]
        public string nian { get; set; }//年份
        [NotMapped]
        public string Time { get; set; }//时间
    }
}
