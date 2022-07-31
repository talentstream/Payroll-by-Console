using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    public class CommissionClassification:PaymentClassification
    {
        private readonly double baseRate;
        private readonly double commissionRate;
        private Hashtable salesReceipts = new Hashtable();

		public CommissionClassification(double baseRate, double commissionRate)
		{
			this.baseRate = baseRate;
			this.commissionRate = commissionRate;
		}
		public double BaseRate
		{
			get { return baseRate; }
		}
		public double CommissionRate
		{
			get { return commissionRate; }
		}

		public void AddSalesReceipt(SalesReceipt receipt)
		{
			salesReceipts[receipt.Date] = receipt;
		}
		public SalesReceipt GetSalesReceipt(DateTime time)
		{
			return salesReceipts[time] as SalesReceipt;
		}
		public override double CalculatePay(Paycheck paycheck)
		//计算总钱，基本工资+销售分成
		{
			double salesTotal = 0;
			foreach (SalesReceipt receipt in salesReceipts.Values)
			{
				if (DateUtil.IsInPayPeriod(receipt.Date,
					paycheck.PayPeriodStartDate,
					paycheck.PayPeriodEndDate))
					salesTotal += receipt.SaleAmount;
			}
			return baseRate + (salesTotal * commissionRate * 0.01);
		}
		public override string ToString()
		{
			return String.Format("${0} + {1}% sales commission", baseRate, commissionRate);
		}
	}
}
