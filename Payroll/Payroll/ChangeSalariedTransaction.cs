using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    public class ChangeSalariedTransaction : ChangeClassificationTransaction
    {
		private readonly double salary;
		public ChangeSalariedTransaction(int id, double salary, PayrollDatabase database)
			: base(id, database)
		{
			this.salary = salary;
		}
		protected override PaymentClassification Classification
		{
			get { return new SalariedClassification(salary); }
		}
		protected override PaymentSchedule Schedule
		{
			get { return new MonthlySchedule(); }
		}
	}
}
