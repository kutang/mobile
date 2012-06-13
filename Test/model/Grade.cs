using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test.Model
{
    #region Grade
    public class Grade
    {
        #region Member Variables
        protected int id;
        protected int level;
        protected DateTime dtime;
        #endregion

        #region Constructors

        public Grade() { }

        #endregion

        #region Public Properties
        public virtual Int32 Id
        {
            get { return id; }
            set { id = value; }
        }
        public virtual Int32 Level
        {
            get { return level; }
            set { level = value; }
        }
        public virtual DateTime Dtime
        {
            get { return dtime; }
            set { dtime = value; }
        }
        #endregion
    }
    #endregion
}
