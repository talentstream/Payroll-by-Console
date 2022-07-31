using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    public class SalesReceiptTransaction:Transaction
    {
		private readonly DateTime date;//日期
		private readonly double saleAmount;//销售额度
		private readonly int empId;//员工id
		public SalesReceiptTransaction(DateTime time, double saleAmount, int empId, PayrollDatabase database)
			: base(database)
		{
			this.date = time;
			this.saleAmount = saleAmount;
			this.empId = empId;
		}
		public override void Execute()
		{
			Employee e = database.GetEmployee(empId);
			if (e != null)
			{
				CommissionClassification hc =
					e.Classification as CommissionClassification;
				if (hc != null)
					hc.AddSalesReceipt(new SalesReceipt(date, saleAmount));
				else
					throw new ApplicationException(
						"Tried to add sales receipt to" +
							"non-commissioned employee");
			}
			else
				throw new ApplicationException(
					"No such employee.");
		}
	}
}
