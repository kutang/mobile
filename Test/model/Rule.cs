using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test.Model
{
    #region
    public class Rule
    {
        #region Member Variables
        protected Int32 ruleid;
        protected Int64 mobilenumber;
        protected Int32 chargeid;

        #endregion
        #region Constructors
        public Rule() { }
        #endregion
        #region Public Properties

        public virtual Int32 Ruleid
        {
            get { return ruleid; }
            set { ruleid = value; }
        }
        public virtual Int64 Mobilenumber
        {
            get { return mobilenumber; }
            set { mobilenumber = value; }
        }
        public virtual Int32 Chargeid
        {
            get { return chargeid; }
            set { chargeid = value; }
        }
        #endregion
    }
    #endregion
}
