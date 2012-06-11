using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;

namespace Test.Model.Dao
{
    public class CustomerDao
    {
        ISession session = null;
        ISessionFactory factory = null;
        ITransaction trans = null;
        public CustomerDao()
        {
            Configuration config = new Configuration().AddAssembly("Test.Model");
            factory = config.BuildSessionFactory();
            session = factory.OpenSession();
        }
        public bool save(Customer customer)
        {
            trans = session.BeginTransaction();
            try
            {
                session.Save(customer);
                trans.Commit();
                return true;
            }
            catch (Exception)
            {
                trans.Rollback();
                return false;
            }
        }
        public Int32 getId()
        {
            trans = session.BeginTransaction();
            try
            {
                Int32 maxid = session.CreateQuery("select max(Customerid) from Customer").UniqueResult<Int32>();
                return maxid;
            }
            catch
            {
                throw new Exception();
            }
        }
        public string getName(Int32 customerId)
        {
            trans = session.BeginTransaction();
            try
            {
                string name = session.CreateQuery("select Name from Customer as c where c.Customerid='" + customerId + "'").UniqueResult<string>();
                return name;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}