using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;

namespace Test.Model.Dao
{
    public class RuleDao
    {
        ISession session = null;
        ISessionFactory factory = null;
        ITransaction trans = null;
        public RuleDao()
        {
            Configuration config = new Configuration().AddAssembly("Test.Model");
            factory = config.BuildSessionFactory();
            session = factory.OpenSession();
        }
        public bool save(Rule rule)
        {
            trans = session.BeginTransaction();
            try
            {
                session.Save(rule);
                trans.Commit();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public Int32 getId()
        {
            trans = session.BeginTransaction();
            try
            {
                Int32 maxid = session.CreateQuery("select max(Ruleid) from Rule").UniqueResult<Int32>();
                return maxid;
            }
            catch
            {
                return -1;
            }
        }
        public bool check(Int64 num,Int32 chargeid)
        {
            trans = session.BeginTransaction();
            try
            {
                Rule rule = session.CreateQuery("from Rule as c where c.Mobilenumber='" + num + "' and c.Chargeid='" + chargeid + "'").UniqueResult<Rule>();
                if (rule == null) return true;
                else return false;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
