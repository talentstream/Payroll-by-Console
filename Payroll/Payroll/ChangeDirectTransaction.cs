using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    public class ChangeDirectTransaction : ChangeMethodTransaction
    {
		public ChangeDirectTransaction(int empId, PayrollDatabase database)
			: base(empId, database)
		{
		}
		protected override PaymentMethod Method
		{
			get { return new DirectDepositMethod("Bank -1", "123"); }
		}
	}
}
