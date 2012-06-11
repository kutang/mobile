using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using System.Collections;

namespace Test.Model.Dao
{
    public class CallRecordDao
    {
        ISession session = null;
        ISessionFactory factory = null;
        ITransaction trans = null;
        public CallRecordDao()
        {
            Configuration config = new Configuration().AddAssembly("Test.Model");
            factory = config.BuildSessionFactory();
            session = factory.OpenSession();
        }

        public void saveRecord(CallRecord callRecord)
        {
            trans = session.BeginTransaction();

            try
            {
                session.Save(callRecord);
                trans.Commit();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public IEnumerable<CallRecord> listCallRecord(Int64 num)
        {
            trans = session.BeginTransaction();
            try
            {
                var hql = @"from CallRecord c
                            where c.FPhoneNumber=:number";
                return session.CreateQuery(hql)
                    .SetString("number", num.ToString())
                    .List<CallRecord>();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public ArrayList findByMonth(string f, string t)
        {
            trans = session.BeginTransaction();
            try
            {
                IEnumerable<CallRecord> ic= session.CreateQuery("from CallRecord").List<CallRecord>();
                ArrayList l = new ArrayList();
                foreach (CallRecord call in ic)
                {
                    if (call.T_from.Month >= Int32.Parse(f) && call.T_to.Month <= Int32.Parse(t))
                        l.Add(call);
                }
                return l;
            }
            catch (Exception)
            {

                throw new Exception();
            }
        }
    }
}