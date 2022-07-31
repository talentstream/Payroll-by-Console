using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    public class DeleteEmployeeTransaction:Transaction
    {
		private readonly int id;//员工id
		public DeleteEmployeeTransaction(int id, PayrollDatabase database)
			: base(database)
		{
			this.id = id;
		}
		public override void Execute()
		{
			database.DeleteEmployee(id);
		}
	}
}
