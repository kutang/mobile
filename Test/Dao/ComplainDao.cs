using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;

namespace Test.Model.Dao
{
    public class ComplainDao
    {
        ISession session = null;
        ISessionFactory factory = null;
        ITransaction trans = null;
        public ComplainDao()
        {
            Configuration config = new Configuration().AddAssembly("Test.Model");
            factory = config.BuildSessionFactory();
            session = factory.OpenSession();
        }
        public bool saveMessage(string message)
        {
            trans = session.BeginTransaction();
            try
            {
                Complain m = new Complain();
                m.Message = message;
                m.Dtime=DateTime.Now;
                session.Save(m);
                trans.Commit();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public IEnumerable<Complain> getMessage()
        {
            trans = session.BeginTransaction();
            try
            {
                return session.CreateQuery("from Complain order by Dtime").List<Complain>();
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
