using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;

namespace Test.Model.Dao
{
    public class ChargeDao
    {
        ISession session = null;
        ISessionFactory factory = null;
        ITransaction trans = null;
        public ChargeDao()
        {
            Configuration config = new Configuration().AddAssembly("Test.Model");
            factory = config.BuildSessionFactory();
            session = factory.OpenSession();
        }

        //开通基本业务
        public Int32 getId(string name,Int32 monthlypay)
        {
            trans = session.BeginTransaction();
            try
            {
                Charge charge=session.CreateQuery("from Charge as c where c.Name='" + name + "' and c.Chargepermonth='" + monthlypay + "'").UniqueResult<Charge>();
                if (charge == null) return -1;
                else return charge.Chargeid;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        //开通其它业务
        public Int32 getId(string name, Int32 monthlypay, int id)
        {
            trans = session.BeginTransaction();
            try
            {
                Charge charge = session.CreateQuery("from Charge as c where c.Name='" + name + "' and c.Chargepermonth='" + monthlypay + "'").UniqueResult<Charge>();
                if (charge == null) return -1;
                else return charge.Chargeid;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public int test()
        {
            trans = session.BeginTransaction();
            try
            {
                Int32 maxid=session.CreateQuery("select max(Chargeid) from Charge").UniqueResult<Int32>();
                return maxid;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
