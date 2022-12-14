using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    public abstract class ChangeAffiliationTransaction : ChangeEmployeeTransaction
    {
		public ChangeAffiliationTransaction(int empId, PayrollDatabase database)
	: base(empId, database)
		{ }
		protected override void Change(Employee e)
		{
			RecordMembership(e);
			Affiliation affiliation = Affiliation;
			e.Affiliation = affiliation;
		}
		protected abstract Affiliation Affiliation { get; }
		protected abstract void RecordMembership(Employee e);

	}
}
