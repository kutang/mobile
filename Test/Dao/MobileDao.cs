using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;

namespace Test.Model.Dao
{
    public class MobileDao
    {
        ISession session = null;
        ISessionFactory factory = null;
        ITransaction trans = null;
        public MobileDao()
        {
            Configuration config = new Configuration().AddAssembly("Test.Model");
            factory = config.BuildSessionFactory();
            session = factory.OpenSession();
        }
        //检查手机号码是否已经存在
        public bool checknumexists(Int64 num)
        {
            trans = session.BeginTransaction();
            try
            {
                var hql = @"from Mobile p
                            where p.Mobilenumber=:mobileNumber";
                Mobile p = session.CreateQuery(hql)
                   .SetString("mobileNumber", num.ToString())
                   .UniqueResult<Mobile>();
                if (p == null) return true;
                else return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //保存用户mobile信息
        public bool save(Mobile mobile)
        {
            trans = session.BeginTransaction();
            try
            {
                session.Save(mobile);
                trans.Commit();
                return true;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        //获取用户mobile信息
        public Mobile getMobile(Int64 num)
        {
            trans = session.BeginTransaction();
            try
            {
                var hql = @"from Mobile p
                            where p.Mobilenumber=:mobileNumber";
                Mobile p = session.CreateQuery(hql)
                  .SetString("mobileNumber", num.ToString())
                  .UniqueResult<Mobile>();
                return p;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        //检查用户余额是否足够
        public bool checkBalance(Int64 num, Int32 chargepermonth)
        {
            trans = session.BeginTransaction();
            try
            {
                float balance = session.CreateQuery("select Balance from Mobile as c where c.Mobilenumber='" + num + "'").UniqueResult<float>();
                if (balance >= chargepermonth) return true;
                else return false;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        //重载checkBalance方法
        public bool checkBalance(Int64 num, float least)
        {
            trans = session.BeginTransaction();
            try
            {
                float balance = session.CreateQuery("select Balance from Mobile as c where c.Mobilenumber='" + num + "'").UniqueResult<float>();
                if (balance >= least) return true;
                else return false;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        } 
        //重载checkBalance方法
        public bool checkBalance(Int64 num, float least, float mus)
        {
            trans = session.BeginTransaction();
            try
            {
                Mobile mobile = session.CreateQuery("from Mobile as c where c.Mobilenumber='" + num + "'").UniqueResult<Mobile>();
                mobile.Balance = mobile.Balance - mus;
                session.Update(mobile);
                trans.Commit();
                if (mobile.Balance >= least) return true;
                else return false;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        //添加业务扣费
        public void koufei(Int64 num, Int32 chargepermonth)
        {
            trans = session.BeginTransaction();
            try
            {
                var hql = @"from Mobile p
                            where p.Mobilenumber=:phoneNumber";
                Mobile p = session.CreateQuery(hql)
                   .SetString("phoneNumber", num.ToString())
                   .UniqueResult<Mobile>();

                p.Balance = p.Balance - chargepermonth;
                session.Update(p);
                trans.Commit();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        //检查用户密码(停机办理过程需要用到)
        public string getPassword(Int64 num)
        {
            trans = session.BeginTransaction();
            try
            {
                var hql = @"from Mobile p
                            where p.Mobilenumber=:phoneNumber";
                Mobile p = session.CreateQuery(hql)
                   .SetString("phoneNumber", num.ToString())
                   .UniqueResult<Mobile>();
                return p.Password;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public void tingjikoufei(Int64 num)
        {
            trans = session.BeginTransaction();
            try
            {
                var hql = @"from Mobile p
                            where p.Mobilenumber=:phoneNumber";
                Mobile p = session.CreateQuery(hql)
                   .SetString("phoneNumber", num.ToString())
                   .UniqueResult<Mobile>();

                p.Balance = p.Balance - 5;
                session.Update(p);
                trans.Commit();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        //检查用户手机状态(on:正常使用,off:欠费,stop:停机状态)
        public bool checkState(Int64 num)
        {
            trans = session.BeginTransaction();
            try
            {
                var hql = @"from Mobile p
                            where p.Mobilenumber=:phoneNumber";
                Mobile p = session.CreateQuery(hql)
                    .SetString("phoneNumber", num.ToString())
                    .UniqueResult<Mobile>();
                if (p.State == "stop")
                    return true;
                else if (p.State == "off")
                    return true;
                else return false;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public void changeState(Int64 num)
        {
            trans = session.BeginTransaction();
            try
            {
                var hql = @"from Mobile p
                            where p.Mobilenumber=:phoneNumber";
                Mobile p = session.CreateQuery(hql)
                    .SetString("phoneNumber", num.ToString())
                    .UniqueResult<Mobile>();
                p.State = "stop";
                trans.Commit();//提交
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
