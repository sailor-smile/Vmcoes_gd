using NurseIndicator.Base.Context;
using System.Collections.Generic;
using Vmcoes_gd.Dao;
using Vmcoes_gd.Dto;
using Vmcoes_gd.Enties;

namespace Vmcoes_gd.Biz.Car
{
    public class VmcoesgdServicelmpl : IVmcoesgdService

    {
        private readonly IVmcoesgdDao _vmcoesgdDao; 
        public VmcoesgdServicelmpl(IVmcoesgdDao vmcoesgd ) {
            _vmcoesgdDao = vmcoesgd;
        }

        public int Delet(int id)
        {
            return _vmcoesgdDao.Delet(id);
        }

        public PageList<VmcoesGd> FindVmcoes(PageCond<Vmparameter> pageCond)
        {
            return _vmcoesgdDao.FindVmcoes(pageCond);
        }

        public PageList<VmcoesGd> FindVmcoesGdorNowadaysTime()
        {
            return _vmcoesgdDao.FindVmcoesGdorNowadaysTime();
        }

        public PageList<VmcoesGd> FindVmcoesGdorTime(Vmparameter pageCond)
        {
            return  _vmcoesgdDao.FindVmcoesGdorTime(pageCond);
        }

        //public PageList<VmcoesGd> FindVmcoesTime(Vmparameter pageCond)
        //{
        //    return _vmcoesgdDao.FindVmcoesTime(pageCond);
        //}

        public VmcoesGd GetVmcoes(int id)
        {
            return _vmcoesgdDao.GetVmcoes(id);
        }

        public int Save(SaveAll vmcoes)
        {
            return _vmcoesgdDao.Save(vmcoes);
        }

        public List<ConstructionSite> Site()
        {
            return _vmcoesgdDao.Site();
        }

        public int Update(VmcoesGd vmcoes)
        {
          return  _vmcoesgdDao.Update(vmcoes);
        }

        public int Updateall(UpdateAll updateAll)
        {
            return _vmcoesgdDao.Updateall(updateAll);
        }
    }
}
