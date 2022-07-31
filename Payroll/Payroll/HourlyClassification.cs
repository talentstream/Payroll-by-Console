using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    public class HourlyClassification : PaymentClassification
    {
        private double hourlyRate;//每一小时的工资
        private Hashtable timeCards = new Hashtable();

        public HourlyClassification(double rate)
        {
            this.hourlyRate = rate;
        }
        public double HourlyRate
        {
            get { return hourlyRate; }
        }
        public TimeCard GetTimeCard(DateTime date)
        {
            return timeCards[date] as TimeCard;
        }
        public void AddTimeCard(TimeCard card)
        {
            timeCards[card.Date] = card;
        }
        public override double CalculatePay(Paycheck paycheck)
        {
            double totoalPay = 0;
            foreach(TimeCard timeCard in timeCards.Values)
            {
                if (DateUtil.IsInPayPeriod(timeCard.Date,
                             paycheck.PayPeriodStartDate,
                            paycheck.PayPeriodEndDate))
                    totoalPay += CalculatePayForTimeCard(timeCard);
            }
            return totoalPay;
        }
        private double CalculatePayForTimeCard(TimeCard card)
        //计算工作时间的钱，加班时间*1.5
        {
            double overtimeHours = Math.Max(0.0, card.Hours - 8);
            double normalHours = card.Hours - overtimeHours;
            return hourlyRate * normalHours +
                hourlyRate * 1.5 * overtimeHours;
        }
        public override string ToString()
        {
            return String.Format("${0}/hr", hourlyRate);
        }
    }
}
