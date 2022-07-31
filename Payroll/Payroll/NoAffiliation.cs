using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    public class NoAffiliation : Affiliation
    {
        double Affiliation.CalculateDeductions(Paycheck paycheck)
        {
            return 0;
        }
    }
}
