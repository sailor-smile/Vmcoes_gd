using NurseIndicator.Dao.Dao;
using System.Linq;
using Vmcoes_gd.Dto;
using Vmcoes_gd.Enties;

namespace Vmcoes_gd.Dao.Users
{
    public class LoginDaolmpl : BaseDaoImpl, ILoginDao
    {
        
        public LoginDaolmpl(VmcoesContext dbContext) : base(dbContext)
        {
        }



        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        //public int updatePwd(UserVmer userVmer)
        //{
           
        //    User user = new User();
        //    user.UId = userVmer.UserId;
        //    user.UName = userVmer.UserName;
        //    user.UPassword = userVmer.newpwd;

            
        //    if (user.UPassword == userVmer.pwd)
        //    {
        //        _dbContext.Attach(user);
        //        _dbContext.Entry(user).Property(po => po.UPassword).IsModified = true;
        //        _dbContext.SaveChanges();
        //        return 1;
        //    }
        //    else if (user.UPassword != userVmer.pwd)
        //    {
        //        return 2;
        //    }
        //    else {
        //        return 0;

        //    }
        //}

        public int updatePwd(string uid, string pwd)
        {
            User user = new User();
            user.UId = uid;
            user.UPassword = pwd;
            _dbContext.Attach(user);
            _dbContext.Entry(user).Property(po => po.UPassword).IsModified = true;
            _dbContext.SaveChanges();
            return 1;
        }



        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User Use(UserVmer vmer)
        {

            var query = from n in _dbContext.User
                        where n.UName == vmer.UserName && n.UPassword == vmer.newpwd
                        select new User {
                            UId = n.UId,
                            UName = n.UName,
                            UPassword = n.UPassword,
                            ActualName = n.ActualName,
                            Status = n.Status
                        };
            
            var site = query.FirstOrDefault();
          
                     return site;
             
        }
    }
}
