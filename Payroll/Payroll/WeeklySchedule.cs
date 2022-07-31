using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    public class WeeklySchedule:PaymentSchedule
    {
		bool PaymentSchedule.IsPayDate(DateTime payDate)
		{
			return payDate.DayOfWeek == DayOfWeek.Friday;
		}
		DateTime PaymentSchedule.GetPayPeriodStartDate(DateTime date)
		{
			return date.AddDays(-6);
		}
		public override string ToString()
		{
			return "weekly";
		}
	}
}
