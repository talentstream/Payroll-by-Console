using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    public class HoldMethod : PaymentMethod
    //保存支票
    {
        void PaymentMethod.Pay(Paycheck paycheck)
        {
            paycheck.SetField("Disposition", "Hold");
        }

        public override string ToString()
        {
            return "hold";
        }
    }
}
