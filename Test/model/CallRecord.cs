using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test.Model
{
    #region CallRecord
    public class CallRecord
    {
       

            #region Member Variables

            protected int _id;
            protected Int64 f_phoneNumber;
            protected Int64 t_phoneNumber;
            protected DateTime t_from;
            protected DateTime t_to;

            #endregion



            #region Constructors

            public CallRecord() { }

            #endregion


            #region Public Properties

            public virtual int Id
            {
                get { return _id; }
                set { _id = value; }
            }

            public virtual Int64 FPhoneNumber
            {
                get { return f_phoneNumber; }
                set {f_phoneNumber = value; }
            }

            public virtual Int64 TPhoneNumber
            {
                get { return t_phoneNumber; }
                set { t_phoneNumber = value; }
            }

            public virtual DateTime T_from
            {
                get { return t_from; }
                set
                {
                    t_from = value;
                }
            }
            public virtual DateTime T_to
            {
                get { return t_to; }
                set { t_to = value; }
            }

            #endregion

    }
    #endregion
}
      
