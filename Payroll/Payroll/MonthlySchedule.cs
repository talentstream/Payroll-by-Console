using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    public class MonthlySchedule : PaymentSchedule
    {
        private bool IsLastDayOfMonth(DateTime date)
        {
            int m1 = date.Month;
            int m2 = date.AddDays(1).Month;
            return (m1 != m2);
            //如果加一天不是同一个月即为这个月的最后一天
        }
        DateTime PaymentSchedule.GetPayPeriodStartDate(DateTime date)
        {
            int days = 0;
            while (date.AddDays(days - 1).Month == date.Month)
                days--;
            return date.AddDays(days);
            //计算这个月有多少天
        }

        bool PaymentSchedule.IsPayDate(DateTime payDate)
        {
            return IsLastDayOfMonth(payDate);
        }
        public override string ToString()
        {
            return "monthly";
        }
    }
}
