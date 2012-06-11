using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test.Model
{
    #region
    public class Customer
    {
        #region Member Variables
        protected Int32 customerid;
        protected string name;
        protected string address;

        #endregion
        #region Constructors
        public Customer() { }
        #endregion
        #region Public Properties
        public virtual Int32 Customerid
        {
            get { return customerid; }
            set { customerid = value; }
        }
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }
        public virtual string Address
        {
            get { return address; }
            set { address = value; }
        }
        #endregion
    }
    #endregion
}
