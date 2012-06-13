using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test.Model
{
    #region Complain
    public class Complain
    {
        #region Member Variables
        protected int id;
        protected DateTime dtime;
        protected string message;

        #endregion

        #region Constructors
        public Complain() { }

        #endregion

        #region Public Properties
        public virtual Int32 Id
        {
            get { return id; }
            set { id = value; }
        }
        public virtual DateTime Dtime
        {
            get { return dtime; }
            set { dtime = value; }
        }
        public virtual string Message
        {
            get { return message; }
            set { message = value; }
        }
        #endregion
    }
    #endregion
}
