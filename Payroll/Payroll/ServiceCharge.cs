using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    public class ServiceCharge
    {
		private readonly DateTime time;
		private readonly double amount;
		public ServiceCharge(DateTime time, double amount)
		{
			this.time = time;
			this.amount = amount;
		}
		public double Amount
		{
			get { return amount; }
		}
		public DateTime Time
		{
			get { return time; }
		}
	}
}
