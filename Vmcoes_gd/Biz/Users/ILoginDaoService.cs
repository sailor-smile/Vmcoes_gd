using Vmcoes_gd.Dto;
using Vmcoes_gd.Enties;

namespace Vmcoes_gd.Biz.Users
{
    public interface ILoginDaoService
    {
        //登录
        User Use(UserVmer vmer);
        //修改密码
        int updatePwd(string uid ,string pwd);
        ////获取用户信息
        //User GetUser();
    }
}
