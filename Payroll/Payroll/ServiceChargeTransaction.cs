using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    public class ServiceChargeTransaction:Transaction
    {
		private readonly int memberId;//ID
		private readonly DateTime time;//次数
		private readonly double charge;//费用
		public ServiceChargeTransaction(int id, DateTime time, double charge, PayrollDatabase database)
			: base(database)
		{
			this.memberId = id;
			this.time = time;
			this.charge = charge;
		}
		public override void Execute()
		{
			Employee e = database.GetUnionMember(memberId);
			if (e != null)
			{
				UnionAffiliation ua = null;
				if (e.Affiliation is UnionAffiliation)
					ua = e.Affiliation as UnionAffiliation;
				if (ua != null)
					ua.AddServiceCharge(
						new ServiceCharge(time, charge));
				else
					throw new ApplicationException(
						"Tries to add service charge to union"
						+ "member without a union affiliation");
			}
			else
				throw new ApplicationException(
					"No such union member.");
		}
	}
}
