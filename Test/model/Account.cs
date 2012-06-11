using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test.Model
{
    #region Account
    public class Account
    {
        #region Member Variables
        protected Int32 id;
        protected Int64 mobilenumber;
        protected Int32 customerid;

        #endregion

        #region Constructors

        public Account() { }

        #endregion

        #region Public Properties

        public virtual int Id
        {
            get { return id; }
            set { id = value; }
        }
        public virtual Int64 Mobilenumber
        {
            get { return mobilenumber; }
            set { mobilenumber = value; }
        }
        public virtual Int32 Customerid
        {
            get { return customerid; }
            set { customerid = value; }
        }
        #endregion
    }
    #endregion
}
