using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;

namespace Test.Model.Dao
{
    public class AccountDao
    {
        ISession session = null;
        ISessionFactory factory = null;
        ITransaction trans = null;
        public AccountDao()
        {
            Configuration config = new Configuration().AddAssembly("Test.Model");
            factory = config.BuildSessionFactory();
            session = factory.OpenSession();
        }
        public bool save(Account account)
        {
            trans = session.BeginTransaction();
            try
            {
                session.Save(account);
                trans.Commit();
                return true;
            }
            catch (Exception)
            {
                trans.Rollback();
                return false;
            }
        }
        public int getCustomerId(Int64 num)
        {
            trans = session.BeginTransaction();
            try
            {
                int customerId = session.CreateQuery("select Customerid from Account as c where c.Mobilenumber='" + num+"'").UniqueResult<Int32>();
                return customerId;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
