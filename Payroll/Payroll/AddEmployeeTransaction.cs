using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    public abstract class AddEmployeeTransaction:Transaction
    {
		private readonly int empid;//员工id
		private readonly string name;//员工姓名
		private readonly string address;//员工地址
		public AddEmployeeTransaction(int empid,
			string name, string address, PayrollDatabase database)
			: base(database)
		{
			this.empid = empid;
			this.name = name;
			this.address = address;
		}
		protected abstract
			PaymentClassification MakeClassification();
		//定义抽象方法，确定支付类型
		protected abstract
			PaymentSchedule MakeSchedule();
		//定义抽象方法，确定支付日期
		public override void Execute()
		//执行添加员工操作，加入数据库
		{
			PaymentClassification pc = MakeClassification();
			PaymentSchedule ps = MakeSchedule();
			PaymentMethod pm = new HoldMethod();
			Employee e = new Employee(empid, name, address);
			e.Classification = pc;
			e.Schedule = ps;
			e.Method = pm;
			database.AddEmployee(e);
		}
		public override string ToString()
		{
			return String.Format("{0}  id:{1}   name:{2}   address:{3}", GetType().Name, empid, name, address);
		}
	}
}
