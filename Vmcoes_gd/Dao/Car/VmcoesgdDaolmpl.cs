using Microsoft.EntityFrameworkCore;
using NurseIndicator.Base.Context;
using NurseIndicator.Base.Enums;
using NurseIndicator.Dao.Dao;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Vmcoes_gd.Dto;
using Vmcoes_gd.Enties;

namespace Vmcoes_gd.Dao
{
    public class VmcoesgdDaolmpl : BaseDaoImpl, IVmcoesgdDao
    {
        public VmcoesgdDaolmpl(VmcoesContext dbContext) : base(dbContext)
        {
        }
       
        /// <summary>
        /// 获取工地名称下拉列表
        /// </summary>
        /// <returns></returns>
        public List<ConstructionSite> Site()
        {
            var query = from n in _dbContext.ConstructionSite
                        where 1 == 1
                        orderby n.ConstCode descending
                        select n;
            var site = query.ToList();
            return site;

        }

        ///// <summary>
        ///// 根据开始和结束时间
        ///// </summary>
        ///// <param name="pageCond"></param>
        ///// <returns></returns>
        //public PageList<VmcoesGd> FindVmcoesTime(Vmparameter pageCond)
        //{
        //    string gq = pageCond.EndTime.Value.ToString("d");
        //    string e = "23:59:59";
        //    string end = gq + " " + e;
        //    var endtime = Convert.ToDateTime(end);

        //    var query = from n in _dbContext.VmcoesGd
        //                where n.InputTime>=pageCond.StartTime && n.InputTime<= endtime
        //                orderby n.ConstCode ascending,n.InputTime descending
        //                select n;

        //    var datas = query.ToList();
        //    foreach (var o in datas)
        //    {
        //        o.Date = o.InputTime.Value.ToString("d");
        //        o.Time = o.InputTime.Value.ToString("t");
        //    }
        //    PageList<VmcoesGd> PageResult = new PageList<VmcoesGd>(0, 0, datas);
        //    //var count = query.Count();
        //    //var data = query.Skip((pageCond.PageNo - 1) * pageCond.PageSize).Take(pageCond.PageSize).ToList();
        //    //PageList<VmcoesGd> PageResult = new PageList<VmcoesGd>(count, pageCond.PageSize, data);
        //    return PageResult;
        //}




        /// <summary>
        /// 根据日期查询
        /// </summary>
        /// <param name="pageCond"></param>
        /// <returns></returns>
        public PageList<VmcoesGd> FindVmcoesGdorTime(Vmparameter pageCond)
        {
                string bq = pageCond.gdrq.Value.ToString("d");

                string s = "00:00:00";
                string e = "23:59:59";
                string state = bq + " " + s;
                string end = bq + " " + e;

                var statetime = Convert.ToDateTime(state);
                var endtime = Convert.ToDateTime(end);

                //string mm = pageCond.Gdrq.Value.Month.ToString();
                //string dd = pageCond.Gdrq.Value.Day.ToString();
                //string mmdd = mm +'.'+dd


                var query = from n in _dbContext.VmcoesGd
                            where n.InputTime >= statetime && n.InputTime <= endtime
                            orderby n.Id descending, n.ConstCode ascending, n.InputTime ascending
                            select n;
                var datas = query.ToList();
                foreach (var o in datas)
                {
                    o.nian = o.InputTime.Value.Year.ToString();
                    o.Datein = o.InputTime.Value.Month.ToString() + '.' + o.InputTime.Value.Day.ToString();
                    o.Date = o.InputTime.Value.ToString("d");
                    o.Time = o.InputTime.Value.ToString("t");
                }
                PageList<VmcoesGd> PageResult = new PageList<VmcoesGd>(0, 0, datas);
                //var count = query.Count();
                //var data = query.Skip((pageCond.PageNo - 1) * pageCond.PageSize).Take(pageCond.PageSize).ToList();
                //PageList<VmcoesGd> PageResult = new PageList<VmcoesGd>(count, pageCond.PageSize, data);
                return PageResult;
            
          
            
        }






        //查询 
        public PageList<VmcoesGd> FindVmcoes(PageCond<Vmparameter> pageCond)
        {
            var query = from n in _dbContext.VmcoesGd
                        where 1 == 1
                        orderby n.ConstCode ascending, n.InputTime descending
                        select n;

            var datas = query.ToList();
            foreach (var o in datas)
            {
                o.Date = o.InputTime.Value.ToString("d");
                o.Time = o.InputTime.Value.ToString("t");
            }
            PageList<VmcoesGd> PageResult = new PageList<VmcoesGd>(0, 0, datas);
            //var count = query.Count();
            //var data = query.Skip((pageCond.PageNo - 1) * pageCond.PageSize).Take(pageCond.PageSize).ToList();
            //PageList<VmcoesGd> PageResult = new PageList<VmcoesGd>(count, pageCond.PageSize, data);
            return PageResult;

        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="vmcoes"></param>
        /// <returns></returns>
        public int Save(SaveAll cond)
        {
            var vms = cond.Vms;
            //foreach (VmcoesGd all in cond.Vms)
            //{
            //    if (GetExist(all.InputTime, all.ConstName)) return (int)EnumDataStatus.EXIST;
            //}
          
            //foreach (var o in vms)
            //{
            //    o.InputTime = Convert.ToDateTime(o.Date +" "+ o.Time);
            //}
          _dbContext.VmcoesGd.AddRange(vms);//添加多行
            _dbContext.SaveChanges();
            return (int)EnumDataStatus.OK;
        }

        public int Updateall(UpdateAll updateAll)
        {
            foreach (VmcoesGd all in updateAll.UpVms)
            {
                Update(all);
            }
            _dbContext.SaveChanges();
            return (int)EnumDataStatus.OK;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="vs"></param>
        /// <returns></returns>
        public int Update(VmcoesGd vs)
        {
            VmcoesGd v = new VmcoesGd();
            v.Id = vs.Id;
            v.InputTime = Convert.ToDateTime(vs.Date + " " + vs.Time);
            v.ConstName = vs.ConstName;
            v.ConstAddr = vs.ConstAddr;
            v.Trans = vs.Trans;
            v.Rinse = vs.Rinse;
            v.Airtight = vs.Airtight;
            v.Certificate = vs.Certificate;
            v.TransportcompanyName = vs.TransportcompanyName;

            //标记上下文
            _dbContext.Attach(v);

            _dbContext.Entry(v).Property(po => po.InputRiqi).IsModified = true;
            _dbContext.Entry(v).Property(po => po.InputTime).IsModified = true;
            _dbContext.Entry(v).Property(po => po.ConstName).IsModified = true;
            _dbContext.Entry(v).Property(po => po.ConstAddr).IsModified = true;
            _dbContext.Entry(v).Property(po => po.Trans).IsModified = true;
            _dbContext.Entry(v).Property(po => po.Rinse).IsModified = true;
            _dbContext.Entry(v).Property(po => po.Airtight).IsModified = true;
            _dbContext.Entry(v).Property(po => po.Certificate).IsModified = true;
            _dbContext.Entry(v).Property(po => po.TransportcompanyName).IsModified = true;

            _dbContext.SaveChanges();
            return (int)EnumDataStatus.OK;

        }

        public int Delet(int id)
        {
            SqlParameter[] sqlParameters = new[] {
                new SqlParameter("@id",id)
        };
        _dbContext.Database.ExecuteSqlCommand("delete from Vmcoes_gd where id = @id", sqlParameters);
            return (int)EnumDataStatus.OK;  
        }

        public VmcoesGd GetVmcoes(int id)
        {
            var query = from n in _dbContext.VmcoesGd
                        where n.Id == id
                        select n;
            var data = query.FirstOrDefault();

            data.Date = data.InputTime.Value.ToString("d");
            data.Time = data.InputTime.Value.ToString("t");
            //VmcoesGd vmcoesGd = new VmcoesGd();
            return data;

        }

        //查询当天时间 
        public PageList<VmcoesGd> FindVmcoesGdorNowadaysTime()
        {
            var query = from n in _dbContext.VmcoesGd
                        where n.InputTime.Value.ToString("d") == DateTime.Now.ToShortDateString().ToString()
                        orderby n.Id descending, n.ConstCode ascending, n.InputTime ascending
                        select n;
            var datas = query.ToList();
            foreach (var o in datas)
            {
                o.nian = o.InputTime.Value.Year.ToString();
                o.Datein = o.InputTime.Value.Month.ToString() + '.' + o.InputTime.Value.Day.ToString();
                o.Date = o.InputTime.Value.ToString("d");
                o.Time = o.InputTime.Value.ToString("t");
            }
            PageList<VmcoesGd> PageResult = new PageList<VmcoesGd>(0, 0, datas);
            //var count = query.Count();
            //var data = query.Skip((pageCond.PageNo - 1) * pageCond.PageSize).Take(pageCond.PageSize).ToList();
            //PageList<VmcoesGd> PageResult = new PageList<VmcoesGd>(count, pageCond.PageSize, data);
            return PageResult;

        }



        ///// <summary>
        ///// 验证数据是否存在，用于限制重复录入
        ///// </summary>
        ///// <param name="InputTime">时间</param>
        ///// <param name="Trans">车辆</param>
        ///// <returns></returns>
        //private bool GetExist(DateTime? InputTime, string ConstName)
        //{
        //    var query = from n in _dbContext.VmcoesGd
        //                where
        //                n.InputTime == InputTime
        //                && n.ConstName == ConstName
        //                select n;
        //    var data = query.FirstOrDefault();
        //    return data != null ? true : false;
        //}






    }
}
