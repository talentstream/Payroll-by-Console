using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    public class SalesReceipt
	//销售凭证
    {
		private readonly DateTime date;//日期
		private readonly double saleAmount;//销售金额
		public SalesReceipt(DateTime date, double amount)
		{
			this.date = date;
			this.saleAmount = amount;
		}
		public DateTime Date
		{
			get { return date; }
		}
		public double SaleAmount
		{
			get { return saleAmount; }
		}
	}
}
