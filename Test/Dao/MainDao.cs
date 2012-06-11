using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;

namespace Test.Model.Dao
{
    public class MainDao
    {
        ISession session = null;
        ISessionFactory factory = null;
        ITransaction trans = null;
        public MainDao()
        {
            Configuration config = new Configuration().AddAssembly("Test.Model");
            factory = config.BuildSessionFactory();
            session = factory.OpenSession();
        }
        public string save(Mobile mobile,Customer customer,Rule rule)
        {
            MobileDao mobileDao = new MobileDao();
            if(!mobileDao.save(mobile)) return "保存mobile失败";
            CustomerDao customerDao = new CustomerDao();
            customerDao.save(customer);
            Int32 customerid = customerDao.getId();
            if (customerid == -1) return "保存Customer失败";
            RuleDao ruleDao = new RuleDao();
            if (!ruleDao.save(rule)) return "保存Rule失败";

            Account account = new Account();
            account.Mobilenumber = mobile.Mobilenumber;
            account.Customerid = customerid;
            AccountDao accountDao = new AccountDao();
            if (!accountDao.save(account)) return "保存Account失败";
            else return "开户成功";
        }
    }
}
