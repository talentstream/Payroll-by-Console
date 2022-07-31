﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    public abstract class PaymentClassification
    {
        public abstract double CalculatePay(Paycheck paycheck);
    }
}
