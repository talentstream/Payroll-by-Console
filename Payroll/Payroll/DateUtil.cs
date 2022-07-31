using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    public class DateUtil//时间计算工具
    {
        public static bool IsInPayPeriod(
            DateTime theDate, DateTime startDate, DateTime endDate)
        {
            return (theDate >= startDate) && (theDate <= endDate);
        }
    }
}
