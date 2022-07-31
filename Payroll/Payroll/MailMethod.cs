using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    public class MailMethod : PaymentMethod
    //邮寄薪水
    {
        private readonly string address;
        public MailMethod(string address)
        {
            this.address = address;
        }
        public string Address
        {
            get { return address; }
        }
        void PaymentMethod.Pay(Paycheck paycheck)
        {
            paycheck.SetField("Disposition", "Mail");
        }
        public override string ToString()
        {
            return String.Format("mail ({0})", address);
        }
    }
}
