using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test.Model
{   
    #region
    public class Charge
    {
        #region Member Variables
        protected Int32 chargeid;
        protected string name;
        protected Int32 chargepermonth;

        #endregion
        #region Constructors
        public Charge() { }
        #endregion
        #region Public Properties
        public virtual Int32 Chargeid
        {
            get { return chargeid; }
            set { chargeid = value; }
        }
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }
        public virtual Int32 Chargepermonth
        {
            get { return chargepermonth; }
            set { chargepermonth = value; }
        }
         #endregion
    }
     #endregion
}
