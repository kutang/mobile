using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test.Model
{
    #region
    public class Mobile
    {
        #region Member Variables
        protected Int32 id;
        protected Int64 mobilenumber;
        protected DateTime datetimeofmakecard;
        protected DateTime lasttimepayfor;
        protected string mobiletype;
        protected float balance;
        protected string state;
        protected string password;

        #endregion
        #region Constructors
        public Mobile() { }
        #endregion
        #region Public Properties
        public virtual int Id
        {
            get { return id; }
            set { id = value; }
        }
        public virtual DateTime LastTimePayFor
        {
            get { return lasttimepayfor; }
            set { lasttimepayfor = value; }
        }
        public virtual string Password
        {
            get { return password; }
            set { password = value; }
        }

        public virtual DateTime DateTimeOfMakeCard
        {
            get { return datetimeofmakecard; }
            set { datetimeofmakecard = value; }
        }

        public virtual string State
        {
            get { return state; }
            set { state = value; }
        }
        public virtual Int64 Mobilenumber
        {
            get { return mobilenumber; }
            set { mobilenumber = value; }
        }
        public virtual string Mobiletype
        {
            get { return mobiletype; }
            set { mobiletype = value; }
        }
        public virtual float Balance
        {
            get { return balance; }
            set { balance = value; }
        }
        #endregion
    }
    #endregion
}
