using Vmcoes_gd.Dao.Users;
using Vmcoes_gd.Dto;
using Vmcoes_gd.Enties;

namespace Vmcoes_gd.Biz.Users
{
    public class LoginDaoServiceImpl : ILoginDaoService
    {
        private readonly ILoginDao _loginDao;
        public  LoginDaoServiceImpl(ILoginDao loginDao)
        {
            _loginDao = loginDao;
        }


        public int updatePwd(string uid, string pwd)
        {
            return _loginDao.updatePwd(uid, pwd);
        }

        public User Use(UserVmer vmer)
        {
            return _loginDao.Use(vmer);
        }
    }
}
