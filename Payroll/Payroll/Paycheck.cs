using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    public class Paycheck
    {
		private DateTime payDate;//支付时间
		private readonly DateTime payPeriodStartDate;//上一次交的时间
		private double grossPay;//总薪水
		private Hashtable fields = new Hashtable();
		private double deductions;//要扣的钱
		private double netPay;//总薪水-要扣的钱

		public Paycheck(DateTime payPeriodStartDate, DateTime payDate)
		{
			this.payDate = payDate;
			this.payPeriodStartDate = payPeriodStartDate;
		}
		public DateTime PayDate
		{
			get { return payDate; }
		}
		public double GrossPay
		{
			get { return grossPay; }
			set { grossPay = value; }
		}
		public double Deductions
		{
			get { return deductions; }
			set { deductions = value; }
		}
		public double NetPay
		{
			get { return netPay; }
			set { netPay = value; }
		}
		public DateTime PayPeriodEndDate
		{
			get { return payDate; }
		}
		public DateTime PayPeriodStartDate
		{
			get { return payPeriodStartDate; }
		}

		public void SetField(string fieldName, string value)
		{
			fields[fieldName] = value;
		}
		public string GetField(string fieldName)
		{
			return fields[fieldName] as string;
		}

	}
}
