﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    public class ChangeHourlyTransaction : ChangeClassificationTransaction
    {
		private readonly double hourlyRate;
		public ChangeHourlyTransaction(int id, double hourlyRate, PayrollDatabase database)
			: base(id, database)
		{
			this.hourlyRate = hourlyRate;
		}
		protected override PaymentClassification Classification
		{
			get { return new HourlyClassification(hourlyRate); }
		}
		protected override PaymentSchedule Schedule
		{
			get { return new WeeklySchedule(); }
		}
	}
}
