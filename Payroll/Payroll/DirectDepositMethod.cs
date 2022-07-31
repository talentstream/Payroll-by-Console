using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    public class DirectDepositMethod:PaymentMethod
    //直接存入银行
	{
		private readonly string bank;//银行
		private readonly string accountNumber;//账户号码
		public DirectDepositMethod(string bank, string accountNumber)
		{
			this.bank = bank;
			this.accountNumber = accountNumber;
		}
		public string Bank
		{
			get { return bank; }
		}
		public string AccountNumber
		{
			get { return accountNumber; }
		}
		void PaymentMethod.Pay(Paycheck paycheck)
        {
			paycheck.SetField("Disposition", "Direct");
		}
		public override string ToString()
		{
			return String.Format("direct deposit into {0}:{1}", bank, accountNumber);
		}
	}
}
