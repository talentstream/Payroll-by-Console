using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    public class TimeCard
    {
		private readonly DateTime date;//日期
		private readonly double hours;//工作时间
		public TimeCard(DateTime date, double hours)
		{
			this.date = date;
			this.hours = hours;
		}
		public double Hours
		{
			get { return hours; }
		}
		public DateTime Date
		{
			get { return date; }
		}
	}
}
