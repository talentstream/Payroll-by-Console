using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    public class Employee//员工类
    {
        private readonly int empid;//员工ID
        private string name;//员工名字
        private string address;//员工地址
        private PaymentClassification classification;//支付种类
        private PaymentSchedule schedule;//支付日期
        private PaymentMethod method;//支付方法
        private Affiliation affiliation = new NoAffiliation();//是否会员

        public Employee(int empid,string name,string address)
        {
            this.empid = empid;
            this.name = name;
            this.address = address;
        }

        public int EmpId
        {
            get { return empid; }
        }

        public string Name
        { 
            get { return name; } 
            set { name = value; } 
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public PaymentClassification Classification
        {
            get { return classification; }
            set { classification = value; }
        }
        public PaymentSchedule Schedule
        {
            get { return schedule; }
            set { schedule = value; }
        }
        public PaymentMethod Method
        {
            get { return method; }
            set { method = value; }
        }
        public Affiliation Affiliation
        {
            get { return affiliation; }
            set { affiliation = value; }
        }

        public bool IsPayDate(DateTime date)
        {
            return schedule.IsPayDate(date);
        }

        public void Payday(Paycheck paycheck)//发薪日
        {
            double grossPay = classification.CalculatePay(paycheck);
            double deductions = affiliation.CalculateDeductions(paycheck);
            double netPay = grossPay - deductions;
            paycheck.GrossPay = grossPay;
            paycheck.Deductions = deductions;
            paycheck.NetPay = netPay;
            method.Pay(paycheck);
        }

        public DateTime GetPayPeriodStartDate(DateTime date)
        {
            return schedule.GetPayPeriodStartDate(date);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            return builder.ToString();
        }
    }
}
