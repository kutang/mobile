using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using System.Collections;

namespace Test.Model.Dao
{
    public class GradeDao
    {
        ISession session = null;
        ISessionFactory factory = null;
        ITransaction trans = null;
        public GradeDao()
        {
            Configuration config = new Configuration().AddAssembly("Test.Model");
            factory = config.BuildSessionFactory();
            session = factory.OpenSession();
        }
        public bool updateGrade(Int32 level)
        {
            trans = session.BeginTransaction();
            try
            {
                int id = 1;
                var hql = @"from Grade p
                            where p.Id=:id";
                Grade p = session.CreateQuery(hql)
                   .SetString("id", id.ToString())
                   .UniqueResult<Grade>();

                p.Level = p.Level+level;
                session.Update(p);
                trans.Commit();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public Int32 getGrade()
        {
            trans = session.BeginTransaction();
            try
            {
                int id = 1;
                var hql = @"from Grade p
                            where p.Id=:id";
                Grade p = session.CreateQuery(hql)
                   .SetString("id", id.ToString())
                   .UniqueResult<Grade>();
                return p.Level;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public bool addGrade(Int32 level)
        {
            trans = session.BeginTransaction();
            try
            {
                Grade g = new Grade();
                g.Dtime = DateTime.Now;
                g.Level = level;
                session.Save(g);
                trans.Commit();
                return true;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        //取出最近评分
        public Int32 getLastGrade()
        {
            trans = session.BeginTransaction();
            try
            {
                Int32 grade = session.CreateQuery("select max(Id) from Grade").UniqueResult<Int32>();
                var hql = @"from Grade p
                            where p.Id=:id";
                Grade p = session.CreateQuery(hql)
                   .SetString("id", grade.ToString())
                   .UniqueResult<Grade>();
                return p.Level;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        //取出当前月份的评分
        public Int32 getMonthGrade()
        {
            trans = session.BeginTransaction();
            try
            {
                IEnumerable<Grade> ic = session.CreateQuery("from Grade").List<Grade>();
                ArrayList l = new ArrayList();
                Int32 level = 0;
                Int32 count = 0;
                foreach (Grade g in ic)
                {
                    if (g.Dtime.Month == DateTime.Now.Month)
                    {
                        level += g.Level;
                        count++;
                    }
                }
                return level;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
