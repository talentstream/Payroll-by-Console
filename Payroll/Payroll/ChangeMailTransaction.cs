using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    public class ChangeMailTransaction : ChangeMethodTransaction
    {
		public ChangeMailTransaction(int empId, PayrollDatabase database)
	: base(empId, database)
		{
		}
		protected override PaymentMethod Method
		{
			get { return new MailMethod("3.14 Pi St"); }
		}
	}
}
