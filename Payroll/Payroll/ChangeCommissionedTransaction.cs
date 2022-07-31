using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    public class ChangeCommissionedTransaction : ChangeClassificationTransaction
    {
		private readonly double baseSalary;
		private readonly double commissionRate;
		public ChangeCommissionedTransaction(int id, double baseSalary, double commissionRate, PayrollDatabase database)
			: base(id, database)
		{
			this.baseSalary = baseSalary;
			this.commissionRate = commissionRate;
		}
		protected override PaymentClassification Classification
		{
			get { return new CommissionClassification(baseSalary, commissionRate); }
		}
		protected override PaymentSchedule Schedule
		{
			get { return new BiWeeklySchedule(); }
		}
	}
}
