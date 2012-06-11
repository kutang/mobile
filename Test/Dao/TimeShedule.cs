using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test.Model
{
    public class TimeShedule
    {
        public bool isPayTime(DateTime time)
        {
            DateTime dt = DateTime.Now;
            DateTime dt1 = Convert.ToDateTime(dt.ToString());
            DateTime dt2 = Convert.ToDateTime(time.ToString());
            TimeSpan span = dt2.Subtract(dt1);
            int dayDiff = span.Days + 1;
            
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int days=0;
            switch (month)
            {
                case 1:
                case 2:
                case 4:
                case 6:
                case 9:
                case 11:
                    if (dayDiff == 32) return true;
                    else return false;
                    
                case 3:
                    if (DateTime.IsLeapYear(year))
                    {
                        days = 29;
                        if (dayDiff == 30) return true;
                        else return false;
                    }
                    else
                    {
                        days = 28;
                        if (dayDiff == 29) return true;
                        else return false;
                    }
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                default :
                    if (dayDiff == 31) return true;
                    else return false;
                
            }
            
            
        }
    }
}
