using NurseIndicator.Base.Context;
using System.Collections.Generic;
using Vmcoes_gd.Dto;
using Vmcoes_gd.Enties;
using Vmcoes_gd.Models;

namespace Vmcoes_gd.Dao
{
    public interface IVmcoesgdDao
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        PageList<VmcoesGd> FindVmcoes(PageCond<Vmparameter> pageCond);
       
        PageList<VmcoesGd> FindVmcoesGdorTime(Vmparameter pageCond);
        PageList<VmcoesGd> FindVmcoesGdorNowadaysTime();

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="vmcoes"></param>
        int Update(VmcoesGd vmcoes);
        int Updateall(UpdateAll updateAll);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="vmcoes"></param>
        int Save(SaveAll cond);

        /// <summary>
        /// 工地查询
        /// </summary>
        /// <returns></returns>
        List<ConstructionSite> Site();

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="vmcoesGd"></param>
        /// <returns></returns>
        int Delet(int id);

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        VmcoesGd GetVmcoes(int id);
    }
}
