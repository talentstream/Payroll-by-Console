using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    public interface PaymentSchedule
    {
        bool IsPayDate(DateTime payDate);//判断是否是发薪日
        DateTime GetPayPeriodStartDate(DateTime date);//计算上一次的发薪日
    }
}
