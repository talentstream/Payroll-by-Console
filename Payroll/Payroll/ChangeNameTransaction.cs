﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    public class ChangeNameTransaction : ChangeEmployeeTransaction
    {
		private readonly string newName;
		public ChangeNameTransaction(int id, string newName, PayrollDatabase database)
			: base(id, database)
		{
			this.newName = newName;
		}
		protected override void Change(Employee e)
		{
			e.Name = newName;
		}
	}
}
