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
        public bool check(Int64 num, Int32 chargeid)
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
        //查询用户的套餐
        public IEnumerable<Rule> getRule(Int64 num)
        {
            trans = session.BeginTransaction();
            try
            {
                IEnumerable<Rule> ie = session.CreateQuery("from Rule as c where c.Mobilenumber='" + num + "'").List<Rule>();
                return ie;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        //取消套餐
        public bool quxiaotaocan(Int64 num, int chargeid)
        {
            trans = session.BeginTransaction();
            try
            {
                Rule rule = session.CreateQuery("from Rule as c where c.Mobilenumber='" + num + "' and c.Chargeid='" + chargeid + "'").UniqueResult<Rule>();
                session.Delete(rule);
                trans.Commit();
                return true;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        //查找用户的基本套餐
        public Int32 getId(Int64 num)
        {
            trans = session.BeginTransaction();
            try
            {
                IEnumerable<Rule> rule = session.CreateQuery("from Rule as c where c.Mobilenumber='" + num + "'").List<Rule>();
                Int32 id = 0;
                foreach (Rule r in rule)
                {
                    if (r.Chargeid == 1)
                    {
                        id = 1;
                        break;
                    }
                    else if (r.Chargeid == 2)
                    {
                        id = 2;
                        break;
                    }
                    else if (r.Chargeid == 3)
                    {
                        id = 3;
                        break;
                    }
                    else continue;
                }
                return id;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public void update(Int64 num, Int32 chargeid)
        {
            trans = session.BeginTransaction();
            try
            {
                IEnumerable<Rule> rule = session.CreateQuery("from Rule as c where c.Mobilenumber='" + num + "'").List<Rule>();
                foreach (Rule r in rule)
                {
                    if (chargeid == 1)
                    {
                        if (r.Chargeid == 2 || r.Chargeid == 3)
                        {
                            r.Chargeid = 1;
                            session.Update(r);
                            trans.Commit();
                            return;
                        }
                    }
                    else if (chargeid == 2)
                    {
                        if (r.Chargeid == 1 || r.Chargeid == 3)
                        {
                            r.Chargeid = 2;
                            session.Update(r);
                            trans.Commit();
                            return;
                        }
                    }
                    else if (chargeid == 3)
                    {
                        if (r.Chargeid == 1 || r.Chargeid == 2)
                        {
                            r.Chargeid = 3;
                            session.Update(r);
                            trans.Commit();
                            return;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
