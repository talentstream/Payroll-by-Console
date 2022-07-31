using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    public class TimeCardTransaction:Transaction
    {
		private readonly DateTime date;//日期
		private readonly double hours;//时间
		private readonly int empId;//员工ID
		public TimeCardTransaction(DateTime date, double hours, int empId, PayrollDatabase database)
			: base(database)
		{
			this.date = date;
			this.hours = hours;
			this.empId = empId;
		}
		public override void Execute()
		{
			Employee e = database.GetEmployee(empId);
			if (e != null)
			{
				HourlyClassification hc =
					e.Classification as HourlyClassification;
				if (hc != null)
					hc.AddTimeCard(new TimeCard(date, hours));
				else
					throw new ApplicationException(
						"Tried to add timecard to" +
							"non-hourly employee");
			}
			else
				throw new ApplicationException(
					"No such employee.");
		}
	}
}
