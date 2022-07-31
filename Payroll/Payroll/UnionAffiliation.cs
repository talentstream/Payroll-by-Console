using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    public class UnionAffiliation:Affiliation
    {
        private Hashtable charges = new Hashtable();
        private int memberId;
        private readonly double dues;

        public UnionAffiliation(int memberId, double dues)
        {
            this.memberId = memberId;
            this.dues = dues;
        }
        
        public UnionAffiliation():this(-1,0.0)
        {

        }

        public double Dues
        {
            get { return dues; }
        }

        public int MemberId
        {
            get { return memberId; }
        }

        public ServiceCharge GetServiceCharge(DateTime time)
        {
            return charges[time] as ServiceCharge;
        }
        public void AddServiceCharge(ServiceCharge sc)
        {
            charges[sc.Time] = sc;
        }

        double Affiliation.CalculateDeductions(Paycheck paycheck)
        //计算要从薪水中扣的钱
        {
            double totalDues = 0;
            int fridays = NumberOfFridaysInPayPeriod(paycheck.PayPeriodStartDate, paycheck.PayPeriodEndDate);
            totalDues = dues * fridays;
            foreach (ServiceCharge charge in charges.Values)
            {
                if (DateUtil.IsInPayPeriod(charge.Time,
                    paycheck.PayPeriodStartDate,
                    paycheck.PayPeriodEndDate))
                    totalDues += charge.Amount;
            }

            return totalDues;
        }

        private int NumberOfFridaysInPayPeriod(DateTime payPeriodStart,DateTime payPeriodEnd)
        //计算交会费之间有多少个周五
        {
            int fridays = 0;
            for (DateTime day = payPeriodStart;day <= payPeriodEnd; day = day.AddDays(1))
            {
                if (day.DayOfWeek == DayOfWeek.Friday)
                    fridays++;
            }
            return fridays;
        }
    }
}
