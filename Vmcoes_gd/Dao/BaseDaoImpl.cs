using Vmcoes_gd.Enties;
/**
*┌──────────────────────────────────────────────────────────────┐
*│　描   述：基础的DAO实现类                                                    
*│　作   者：Jzg                                              
*│　版   本：1.0                                                 
*│　创建时间：2018-12-19 0:18:43                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间: NurseIndicator.Dal.Dao                                   
*│　类   名：BaseDaoImpl                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace NurseIndicator.Dao.Dao
{
    public abstract class BaseDaoImpl
    {
        protected VmcoesContext _dbContext;
        /// <summary>
        /// 注入数据上下文对象
        /// </summary>
        /// <param name="dbContext"></param>
        public BaseDaoImpl(VmcoesContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BaseDaoImpl()
        {
            _dbContext = new VmcoesContext();
        }


    }
}
