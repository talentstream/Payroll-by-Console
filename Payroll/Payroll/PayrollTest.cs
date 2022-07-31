using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    public class PayrollTest
    {
		private PayrollDatabase database;
		public void SetUp()
		{
			database = new InMemoryPayrollDatabase();
		}

		//测试添加薪水员工
		public void TestAddSalariedEmployee()
		{
			int empId = 1;
			AddSalariedEmployee t =
				new AddSalariedEmployee(empId, "Bob", "Home", 1000.00, database);
			t.Execute();
			Employee e = database.GetEmployee(empId);
            PaymentClassification pc = e.Classification;
			SalariedClassification sc = pc as SalariedClassification;
			PaymentSchedule ps = e.Schedule;
			PaymentMethod pm = e.Method;
			if (e.Name == "Bob"
				&& pc is SalariedClassification
				&& ps is MonthlySchedule
				&& pm is HoldMethod)
			{
				Console.WriteLine("TestAddSalariedEmployee test succeed");
			}
			else
			{
				Console.WriteLine("TestAddSalariedEmployee test error");
			}
		}
		//测试添加钟点工
		public void TestAddHourlyEmployee()
		{
			int empId = 2;
			AddHourlyEmployee t =
				new AddHourlyEmployee(empId, "Micah", "Home", 200.00, database);
			t.Execute();
			Employee e = database.GetEmployee(empId);
			PaymentClassification pc = e.Classification;
			HourlyClassification hc = pc as HourlyClassification;
			PaymentSchedule ps = e.Schedule;
			PaymentMethod pm = e.Method;
			if (e.Name == "Micah"
				&& pc is HourlyClassification
				&& ps is WeeklySchedule
				&& pm is HoldMethod)
			{
				Console.WriteLine("TestAddHourlyEmployee test succeed");
			}
			else
			{
				Console.WriteLine("TestAddHourlyEmployee test error");
			}
		}
		//测试添加工会员工
		public void TestAddCommissionedEmployee()
		{
			int empId = 3;
			AddCommissionedEmployee t =
				new AddCommissionedEmployee(empId, "Justin", "Home", 2500, 9.5, database);
			t.Execute();
			Employee e = database.GetEmployee(empId);
			PaymentClassification pc = e.Classification;
			CommissionClassification cc = pc as CommissionClassification;
			PaymentSchedule ps = e.Schedule;
			PaymentMethod pm = e.Method;
			if (e.Name == "Justin"
				&& pc is CommissionClassification
				&& ps is BiWeeklySchedule
				&& pm is HoldMethod)
			{
				Console.WriteLine("TestAddCommissionedEmployee test succeed");
			}
			else
			{
				Console.WriteLine("TestAddCommissionedEmployee test error");
			}
		}
		//测试删除员工
		public void TestDeleteEmplyee()
		{
			int empId = 4;
			AddCommissionedEmployee t =
				new AddCommissionedEmployee(
					empId, "Bill", "Home", 2500, 3.2, database);
			t.Execute();
			Employee e = database.GetEmployee(empId);
			DeleteEmployeeTransaction dt =
				new DeleteEmployeeTransaction(empId, database);
			dt.Execute();
			e = database.GetEmployee(empId);
			if (e==null)
			{
				Console.WriteLine("TestDeleteEmplyeee test succeed");
			}
			else
			{
				Console.WriteLine("TestDeleteEmplyee test error");
			}
		}
		//测试时间卡
		public void TestTimeCardTransaction()
        {
			int empId = 5;
			AddHourlyEmployee t =
				new AddHourlyEmployee(empId, "Bill", "Home", 15.25, database);
			t.Execute();
			TimeCardTransaction tct =
				new TimeCardTransaction(
					new DateTime(2005, 7, 31), 8.0, empId, database);
			tct.Execute();
			Employee e = database.GetEmployee(empId);
			PaymentClassification pc = e.Classification;
			HourlyClassification hc = pc as HourlyClassification;
			TimeCard tc = hc.GetTimeCard(new DateTime(2005, 7, 31));
			if (e != null
				&& pc is HourlyClassification
				&& tc != null
				&& tc.Hours == 8.0)
			{
				Console.WriteLine("TestTimeCardTransaction test succeed");
			}
			else
			{
				Console.WriteLine("TestTimeCardTransaction test error");
			}
		}
		//测试销售凭证
		public void TestSalesReceiptTransaction()
		{
			int empId = 5;
			AddCommissionedEmployee t =
				new AddCommissionedEmployee(
					empId, "Bill", "Home", 2000, 15.25, database);
			t.Execute();
			SalesReceiptTransaction tct =
				new SalesReceiptTransaction(
					new DateTime(2005, 7, 31), 250.00, empId, database);
			tct.Execute();
			Employee e = database.GetEmployee(empId);
			PaymentClassification pc = e.Classification;
			CommissionClassification cc = pc as CommissionClassification;
			SalesReceipt sr = cc.GetSalesReceipt(new DateTime(2005, 7, 31));
			if (e != null
				&& pc is CommissionClassification
				&& sr != null
				&& Math.Abs(sr.SaleAmount - 250.0) < 0.001 )
			{
				Console.WriteLine("TestSalesReceiptTransaction test succeed");
			}
			else
			{
				Console.WriteLine("TestSalesReceiptTransaction test error");
			}
		}
		//测试添加工会服务费用
		public void TestAddServiceChargeTransaction()
		{
			int empId = 2;
			AddHourlyEmployee t = new AddHourlyEmployee(
				empId, "Bill", "Home", 15.25, database);
			t.Execute();
			Employee e = database.GetEmployee(empId);
			UnionAffiliation af = new UnionAffiliation();
			e.Affiliation = af;
			int memberId = 86; // Maxwell Smart
			database.AddUnionMember(memberId, e);
			ServiceChargeTransaction sct =
				new ServiceChargeTransaction(
					memberId, new DateTime(2005, 8, 8), 12.95, database);
			sct.Execute();
			ServiceCharge sc =
				af.GetServiceCharge(new DateTime(2005, 8, 8));
			if (e != null
				&& sc != null
				&& Math.Abs(sc.Amount - 12.95) < 0.001)
			{
				Console.WriteLine("TestAddServiceChargeTransaction test succeed");
			}
			else
			{
				Console.WriteLine("TestAddServiceChargeTransaction test error");
			}
		}
		//测试修改员工名字
		public void TestChangeNameTransaction()
		{
			int empId = 2;
			AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25, database);
			t.Execute();
			ChangeNameTransaction cnt = new ChangeNameTransaction(empId, "Bob", database);
			cnt.Execute();
			Employee e = database.GetEmployee(empId);
			if (e != null
				&& e.Name == "Bob")
			{
				Console.WriteLine("TestChangeNameTransaction test succeed");
			}
			else
			{
				Console.WriteLine("TestChangeNameTransaction test error");
			}
		}
		//测试修改员工地址
		public void TestChangeAddressTransaction()
        {
			int empId = 2;
			AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25, database);
			t.Execute();
			ChangeAddressTransaction cat = new ChangeAddressTransaction(empId, "Hoime", database);
			cat.Execute();
			Employee e = database.GetEmployee(empId); 
			if (e != null
				&& e.Address == "Hoime")
			{
				Console.WriteLine("TestChangeAddressTransaction test succeed");
			}
			else
			{
				Console.WriteLine("TestChangeAddressTransaction test error");
			}
		}
		//测试变为钟点工
		public void TestChangeHourlyTransaction()
		{
			int empId = 3;
			AddCommissionedEmployee t =
				new AddCommissionedEmployee(
					empId, "Lance", "Home", 2500, 3.2, database);
			t.Execute();
			ChangeHourlyTransaction cht =
				new ChangeHourlyTransaction(empId, 27.52, database);
			cht.Execute();
			Employee e = database.GetEmployee(empId);
			PaymentClassification pc = e.Classification;
			HourlyClassification hc = pc as HourlyClassification;
			PaymentSchedule ps = e.Schedule;
			if (e != null
				&& pc is HourlyClassification
				&& ps is WeeklySchedule
				&& Math.Abs(hc.HourlyRate-27.52)<0.001)
			{
				Console.WriteLine("TestChangeHourlyTransaction test succeed");
			}
			else
			{
				Console.WriteLine("TestChangeHourlyTransaction test error");
			}
		}
		//测试变为薪水员工
		public void TestChangeSalaryTransaction()
		{
			int empId = 4;
			AddCommissionedEmployee t =
				new AddCommissionedEmployee(
					empId, "Lance", "Home", 2500, 3.2, database);
			t.Execute();
			ChangeSalariedTransaction cst =
				new ChangeSalariedTransaction(empId, 3000.00, database);
			cst.Execute();
			Employee e = database.GetEmployee(empId);
			PaymentClassification pc = e.Classification;
			SalariedClassification sc = pc as SalariedClassification;
			PaymentSchedule ps = e.Schedule;
			if (e != null
				&& pc is SalariedClassification
				&& ps is MonthlySchedule
				&& Math.Abs(sc.Salary - 3000.00) < 0.001)
			{
				Console.WriteLine("TestChangeSalaryTransaction test succeed");
			}
			else
			{
				Console.WriteLine("TestChangeSalaryTransaction test error");
			}
		}
		//测试变为工会员工
		public void TestChangeCommisionTransaction()
        {
			int empId = 5;
			AddSalariedEmployee t =
				new AddSalariedEmployee(
					empId, "Bob", "Home", 2500.00, database);
			t.Execute();
			ChangeCommissionedTransaction cht =
				new ChangeCommissionedTransaction(empId, 1250.00, 5.6, database);
			cht.Execute();
			Employee e = database.GetEmployee(empId);
			PaymentClassification pc = e.Classification;
			CommissionClassification cc = pc as CommissionClassification;
			PaymentSchedule ps = e.Schedule;
			if (e != null
				&& pc is CommissionClassification
				&& ps is BiWeeklySchedule
				&& Math.Abs(cc.BaseRate - 1250.00) < 0.001
				&& Math.Abs(cc.CommissionRate - 5.6) < 0.001)
			{
				Console.WriteLine("TestChangeCommisionTransaction test succeed");
			}
			else
			{
				Console.WriteLine("TestChangeCommisionTransaction test error");
			}
		}
		//测试变为直接存入银行方法
		public void ChangeDirectDepositMethod()
		{
			int empId = 6;
			AddSalariedEmployee t =
				new AddSalariedEmployee(
					empId, "Mike", "Home", 3500.00, database);
			t.Execute();
			ChangeDirectTransaction cddt =
				new ChangeDirectTransaction(empId, database);
			cddt.Execute();
			Employee e = database.GetEmployee(empId);
			PaymentMethod method = e.Method;
			if(method is DirectDepositMethod)
            {
				Console.WriteLine("ChangeDirectDepositMethod test succeed");
            }
			else
            {
				Console.WriteLine("ChangeDirectDepositMethod test error");
            }
		}
		//测试先存支票方法
		public void ChangeHoldMethod()
		{
			int empId = 7;
			AddSalariedEmployee t =
				new AddSalariedEmployee(
					empId, "Mike", "Home", 3500.00, database);
			t.Execute();
			new ChangeDirectTransaction(empId, database).Execute();
			ChangeHoldTransaction cht =
				new ChangeHoldTransaction(empId, database);
			cht.Execute();
			Employee e = database.GetEmployee(empId);
			PaymentMethod method = e.Method;
			if (method is HoldMethod)
			{
				Console.WriteLine("ChangeHoldMethod test succeed");
			}
			else
			{
				Console.WriteLine("ChangeHoldMethod test error");
			}
		}
		//测试邮寄方法
		public void ChangeMailMethod()
		{
			int empId = 8;
			AddSalariedEmployee t =
				new AddSalariedEmployee(
					empId, "Mike", "Home", 3500.00, database);
			t.Execute();
			ChangeMailTransaction cmt =
				new ChangeMailTransaction(empId, database);
			cmt.Execute();
			Employee e = database.GetEmployee(empId);
			PaymentMethod method = e.Method;
			if (method is MailMethod)
			{
				Console.WriteLine("ChangeMailMethod test succeed");
			}
			else
			{
				Console.WriteLine("ChangeMailMethod test error");
			}
		}
		//测试变为工会成员
		public void ChangeUnionMember()
		{
			int empId = 9;
			AddHourlyEmployee t =
				new AddHourlyEmployee(empId, "Bill", "Home", 15.25, database);
			t.Execute();
			int memberId = 7743;
			ChangeMemberTransaction cmt =
				new ChangeMemberTransaction(empId, memberId, 99.42, database);
			cmt.Execute();
			Employee e = database.GetEmployee(empId);
			Affiliation affiliation = e.Affiliation;
			UnionAffiliation uf = affiliation as UnionAffiliation;
			Employee member = database.GetUnionMember(memberId);
			if(e == member && affiliation is UnionAffiliation && Math.Abs(uf.Dues-99.42)<0.001)
            {
				Console.WriteLine("ChangeUnionMember test succeed");
			}
			else
            {
				Console.WriteLine("ChangeUnionMember test error");
			}
		}
		//测试解除工会成员
		public void ChangeUnaffiliatedMember()
		{
			int empId = 10;
			AddHourlyEmployee t =
				new AddHourlyEmployee(empId, "Bill", "Home", 15.25, database);
			t.Execute();
			int memberId = 7743;
			new ChangeMemberTransaction(empId, memberId, 99.42, database).Execute();
			ChangeUnaffiliatedTransaction cut =
				new ChangeUnaffiliatedTransaction(empId, database);
			cut.Execute();
			Employee e = database.GetEmployee(empId);
			Affiliation affiliation = e.Affiliation;
			Employee member = database.GetUnionMember(memberId);
            if (affiliation is NoAffiliation && member == null)
            {
				Console.WriteLine("ChangeUnaffiliatedMember test succeed");
			}
			else
            {
				Console.WriteLine("ChangeUnaffiliatedMember test error");
			}
        }
        //测试支付给薪水员工工资
        public void PaySingleSalariedEmployee()
		{
			int empId = 1;
			AddSalariedEmployee t = new AddSalariedEmployee(
				empId, "Bob", "Home", 1000.00, database);
			t.Execute();
			DateTime payDate = new DateTime(2001, 11, 30);
			PaydayTransaction pt = new PaydayTransaction(payDate, database);
			pt.Execute();
			Paycheck pc = pt.GetPaycheck(empId);
			if(payDate == pc.PayDate 
				&& Math.Abs(pc.GrossPay-1000.0)<0.001
				&&Math.Abs(pc.Deductions-0.0)<0.001
				&&Math.Abs(pc.NetPay-1000.0)<0.001)
            {
				Console.WriteLine("PaySingleSalariedEmployee test succeed");
			}
			else
            {
				Console.WriteLine("PaySingleSalariedEmployee test error");
			}
		}
		//测试付给薪水员工工资在错误的日期
		public void PaySingleSalariedEmployeeOnWrongDate()
		{
			int empId = 1;
			AddSalariedEmployee t = new AddSalariedEmployee(
				empId, "Bob", "Home", 1000.00, database);
			t.Execute();
			DateTime payDate = new DateTime(2001, 11, 29);
			PaydayTransaction pt = new PaydayTransaction(payDate, database);
			pt.Execute();
			Paycheck pc = pt.GetPaycheck(empId);
			if(pc == null)
            {
				Console.WriteLine("PaySingleSalariedEmployeeOnWrongDate test succeed");
            }
			else
            {
				Console.WriteLine("PaySingleSalariedEmployeeOnWrongDate test error");
            }
		}
		//测试支付没有时间卡的钟点工工资
		public void PayingSingleHourlyEmployeeNoTimeCards()
		{
			int empId = 2;
			AddHourlyEmployee t = new AddHourlyEmployee(
				empId, "Bill", "Home", 15.25, database);
			t.Execute();
			DateTime payDate = new DateTime(2001, 11, 9); // Friday
			PaydayTransaction pt = new PaydayTransaction(payDate, database);
			pt.Execute();
			Console.Write("PayingSingleHourlyEmployeeNoTimeCards");
			ValidatePaycheck(pt, empId, payDate, 0.0);
		}
		private void ValidatePaycheck(PaydayTransaction pt,
									  int empid, DateTime payDate, double pay)
		{
			Paycheck pc = pt.GetPaycheck(empid);
			if(pc != null 
				&& payDate == pc.PayDate
				&& Math.Abs(pc.GrossPay - pay)<0.001
				&& Math.Abs(pc.Deductions - 0.0)<0.001
				&& Math.Abs(pc.NetPay-pay)<0.001)
            {
				Console.WriteLine(" test succeed");
			}
			else
            {
				Console.WriteLine(" test error");
            }
		}
		//测试支付只有一张时间卡的钟点工工资
		public void PaySingleHourlyEmployeeOneTimeCard()
		{
			int empId = 2;
			AddHourlyEmployee t = new AddHourlyEmployee(
				empId, "Bill", "Home", 15.25, database);
			t.Execute();
			DateTime payDate = new DateTime(2001, 11, 9); // Friday
			TimeCardTransaction tc =
				new TimeCardTransaction(payDate, 2.0, empId, database);
			tc.Execute();
			PaydayTransaction pt = new PaydayTransaction(payDate, database);
			pt.Execute();
			Console.Write("PaySingleHourlyEmployeeOneTimeCard");
			ValidatePaycheck(pt, empId, payDate, 30.5);
		}
		//测试支付加班时间卡钟点工工资
		public void PaySingleHourlyEmployeeOvertimeOneTimeCard()
		{
			int empId = 2;
			AddHourlyEmployee t = new AddHourlyEmployee(
				empId, "Bill", "Home", 15.25, database);
			t.Execute();
			DateTime payDate = new DateTime(2001, 11, 9); // Friday
			TimeCardTransaction tc =
				new TimeCardTransaction(payDate, 9.0, empId, database);
			tc.Execute();
			PaydayTransaction pt = new PaydayTransaction(payDate, database);
			pt.Execute();
			Console.Write("PaySingleHourlyEmployeeOvertimeOneTimeCard");
			ValidatePaycheck(pt, empId, payDate, (8 + 1.5) * 15.25);
		}
		//测试在错误日期支付钟点工工资
		public void PaySingleHourlyEmployeeOnWrongDate()
		{
			int empId = 2;
			AddHourlyEmployee t = new AddHourlyEmployee(
				empId, "Bill", "Home", 15.25, database);
			t.Execute();
			DateTime payDate = new DateTime(2001, 11, 8); // Thursday
			TimeCardTransaction tc =
				new TimeCardTransaction(payDate, 9.0, empId, database);
			tc.Execute();
			PaydayTransaction pt = new PaydayTransaction(payDate, database);
			pt.Execute();
			Paycheck pc = pt.GetPaycheck(empId);
			if (pc == null)
            {
				Console.WriteLine("PaySingleHourlyEmployeeOnWrongDate test succeed");
            }
			else
            {
				Console.WriteLine("PaySingleHourlyEmployeeOnWrongDate test error");
            }
		}
		//测试支付给有两张时间卡的钟点工
		public void PaySingleHourlyEmployeeTwoTimeCards()
		{
			int empId = 2;
			AddHourlyEmployee t = new AddHourlyEmployee(
				empId, "Bill", "Home", 15.25, database);
			t.Execute();
			DateTime payDate = new DateTime(2001, 11, 9); // Friday
			TimeCardTransaction tc =
				new TimeCardTransaction(payDate, 2.0, empId, database);
			tc.Execute();
			TimeCardTransaction tc2 =
				new TimeCardTransaction(payDate.AddDays(-1), 5.0, empId, database);
			tc2.Execute();
			PaydayTransaction pt = new PaydayTransaction(payDate, database);
			pt.Execute();
			Console.Write("PaySingleHourlyEmployeeTwoTimeCards");
			ValidatePaycheck(pt, empId, payDate, 7 * 15.25);
		}
		//测试支付钟点工在一段时间的工资
		public void
			TestPaySingleHourlyEmployeeWithTimeCardsSpanningTwoPayPeriods()
		{
			int empId = 2;
			AddHourlyEmployee t = new AddHourlyEmployee(
				empId, "Bill", "Home", 15.25, database);
			t.Execute();
			DateTime payDate = new DateTime(2001, 11, 9); // Friday
			DateTime dateInPreviousPayPeriod =
				new DateTime(2001, 10, 30);
			TimeCardTransaction tc =
				new TimeCardTransaction(payDate, 2.0, empId, database);
			tc.Execute();
			TimeCardTransaction tc2 = new TimeCardTransaction(
				dateInPreviousPayPeriod, 5.0, empId, database);
			tc2.Execute();
			PaydayTransaction pt = new PaydayTransaction(payDate, database);
			pt.Execute();
			Console.Write("TestPaySingleHourlyEmployeeWithTimeCardsSpanningTwoPayPeriods");
			ValidatePaycheck(pt, empId, payDate, 2 * 15.25);
		}
		//测试给没有销售凭证的会员员工工资
		public void PayingSingleCommissionedEmployeeNoReceipts()
		{
			int empId = 2;
			AddCommissionedEmployee t = new AddCommissionedEmployee(
				empId, "Bill", "Home", 1500, 10, database);
			t.Execute();
			DateTime payDate = new DateTime(2001, 11, 16); // Payday
			PaydayTransaction pt = new PaydayTransaction(payDate, database);
			pt.Execute();
			Console.Write("PayingSingleCommissionedEmployeeNoReceipts");
			ValidatePaycheck(pt, empId, payDate, 1500.0);
		}
		//测试给有销售凭证的会员员工工资
		public void PaySingleCommissionedEmployeeOneReceipt()
		{
			int empId = 2;
			AddCommissionedEmployee t = new AddCommissionedEmployee(
				empId, "Bill", "Home", 1500, 10, database);
			t.Execute();
			DateTime payDate = new DateTime(2001, 11, 16); // Payday
			SalesReceiptTransaction sr =
				new SalesReceiptTransaction(payDate, 5000.00, empId, database);
			sr.Execute();
			PaydayTransaction pt = new PaydayTransaction(payDate, database);
			pt.Execute();
			Console.Write("PaySingleCommissionedEmployeeOneReceipt");
			ValidatePaycheck(pt, empId, payDate, 2000.00);
		}
		//测试在错误的日期支付给会员员工工资
		public void PaySingleCommissionedEmployeeOnWrongDate()
		{
			int empId = 2;
			AddCommissionedEmployee t = new AddCommissionedEmployee(
				empId, "Bill", "Home", 1500, 10, database);
			t.Execute();
			DateTime payDate = new DateTime(2001, 11, 9); // wrong friday
			SalesReceiptTransaction sr =
				new SalesReceiptTransaction(payDate, 5000.00, empId, database);
			sr.Execute();
			PaydayTransaction pt = new PaydayTransaction(payDate, database);
			pt.Execute();
			Paycheck pc = pt.GetPaycheck(empId);
			if (pc == null)
			{
				Console.WriteLine("PaySingleCommissionedEmployeeOnWrongDate test succeed");
			}
			else
			{
				Console.WriteLine("PaySingleCommissionedEmployeeOnWrongDate test error");
			}
		}
		//测试给有两张销售凭证的会员员工工资
		public void PaySingleCommissionedEmployeeTwoReceipts()
		{
			int empId = 2;
			AddCommissionedEmployee t = new AddCommissionedEmployee(
				empId, "Bill", "Home", 1500, 10, database);
			t.Execute();
			DateTime payDate = new DateTime(2001, 11, 16); // Payday
			SalesReceiptTransaction sr =
				new SalesReceiptTransaction(payDate, 5000.00, empId, database);
			sr.Execute();
			SalesReceiptTransaction sr2 = new SalesReceiptTransaction(
				payDate.AddDays(-1), 3500.00, empId, database);
			sr2.Execute();
			PaydayTransaction pt = new PaydayTransaction(payDate, database);
			pt.Execute();
			Console.Write("PaySingleCommissionedEmployeeTwoReceipts");
			ValidatePaycheck(pt, empId, payDate, 2350.00);
		}
		//测试支付给会员员工一段时间的工资
		public void
			TestPaySingleCommissionedEmployeeWithReceiptsSpanningTwoPayPeriods()
		{
			int empId = 2;
			AddCommissionedEmployee t = new AddCommissionedEmployee(
				empId, "Bill", "Home", 1500, 10, database);
			t.Execute();
			DateTime payDate = new DateTime(2001, 11, 16); // Payday
			SalesReceiptTransaction sr =
				new SalesReceiptTransaction(payDate, 5000.00, empId, database);
			sr.Execute();
			SalesReceiptTransaction sr2 = new SalesReceiptTransaction(
				payDate.AddDays(-15), 3500.00, empId, database);
			sr2.Execute();
			PaydayTransaction pt = new PaydayTransaction(payDate, database);
			pt.Execute();
			Console.Write("TestPaySingleCommissionedEmployeeWithReceiptsSpanningTwoPayPeriods");
			ValidatePaycheck(pt, empId, payDate, 2000.00);
		}
		//测试薪水员工工会扣费
		public void SalariedUnionMemberDues()
		{
			int empId = 1;
			AddSalariedEmployee t = new AddSalariedEmployee(
				empId, "Bob", "Home", 1000.00, database);
			t.Execute();
			int memberId = 7734;
			ChangeMemberTransaction cmt =
				new ChangeMemberTransaction(empId, memberId, 9.42, database);
			cmt.Execute();
			DateTime payDate = new DateTime(2001, 11, 30);
			PaydayTransaction pt = new PaydayTransaction(payDate, database);
			pt.Execute();
			Paycheck pc = pt.GetPaycheck(empId);
			if (pc != null
				&& payDate == pc.PayDate
				&& Math.Abs(pc.GrossPay - 1000.0) < 0.001
				&& Math.Abs(pc.Deductions - 47.1) < 0.001
				&& Math.Abs(pc.NetPay - (1000.0-47.1)) < 0.001)
			{
				Console.WriteLine("SalariedUnionMemberDues test succeed");
			}
			else
			{
				Console.WriteLine("SalariedUnionMemberDues test error");
			}
		}
		//测试钟点工工会扣除一定服务费用
		public void HourlyUnionMemberServiceCharge()
		{
			int empId = 1;
			AddHourlyEmployee t = new AddHourlyEmployee(
				empId, "Bill", "Home", 15.24, database);
			t.Execute();
			int memberId = 7734;
			ChangeMemberTransaction cmt =
				new ChangeMemberTransaction(empId, memberId, 9.42, database);
			cmt.Execute();
			DateTime payDate = new DateTime(2001, 11, 9);
			ServiceChargeTransaction sct =
				new ServiceChargeTransaction(memberId, payDate, 19.42, database);
			sct.Execute();
			TimeCardTransaction tct =
				new TimeCardTransaction(payDate, 8.0, empId, database);
			tct.Execute();
			PaydayTransaction pt = new PaydayTransaction(payDate, database);
			pt.Execute();
			Paycheck pc = pt.GetPaycheck(empId);
			if (pc != null
				&& payDate == pc.PayPeriodEndDate
				&& Math.Abs(pc.GrossPay - 8 * 15.24) < 0.001
				&& Math.Abs(pc.Deductions - (9.42 + 19.42)) < 0.001
				&& Math.Abs(pc.NetPay - ((8 * 15.24) - (9.42 + 19.42))) < 0.001)
			{
				Console.WriteLine("HourlyUnionMemberServiceCharge test succeed");
			}
			else
			{
				Console.WriteLine("HourlyUnionMemberServiceCharge test error");
			}
		}
		//测试多种支付混合
		public void ServiceChargesSpanningMultiplePayPeriods()
		{
			int empId = 1;
			AddHourlyEmployee t = new AddHourlyEmployee(
				empId, "Bill", "Home", 15.24, database);
			t.Execute();
			int memberId = 7734;
			ChangeMemberTransaction cmt =
				new ChangeMemberTransaction(empId, memberId, 9.42, database);
			cmt.Execute();
			DateTime payDate = new DateTime(2001, 11, 9);
			DateTime earlyDate =
				new DateTime(2001, 11, 2); // previous Friday
			DateTime lateDate =
				new DateTime(2001, 11, 16); // next Friday
			ServiceChargeTransaction sct =
				new ServiceChargeTransaction(memberId, payDate, 19.42, database);
			sct.Execute();
			ServiceChargeTransaction sctEarly =
				new ServiceChargeTransaction(memberId, earlyDate, 100.00, database);
			sctEarly.Execute();
			ServiceChargeTransaction sctLate =
				new ServiceChargeTransaction(memberId, lateDate, 200.00, database);
			sctLate.Execute();
			TimeCardTransaction tct =
				new TimeCardTransaction(payDate, 8.0, empId, database);
			tct.Execute();
			PaydayTransaction pt = new PaydayTransaction(payDate, database);
			pt.Execute();
			Paycheck pc = pt.GetPaycheck(empId);
			if (pc != null
				&& payDate == pc.PayPeriodEndDate
				&& Math.Abs(pc.GrossPay - 8 * 15.24) < 0.001
				&& Math.Abs(pc.Deductions - (9.42 + 19.42)) < 0.001
				&& Math.Abs(pc.NetPay - ((8 * 15.24) - (9.42 + 19.42))) < 0.001)
			{
				Console.WriteLine("ServiceChargesSpanningMultiplePayPeriods test succeed");
			}
			else
			{
				Console.WriteLine("ServiceChargesSpanningMultiplePayPeriods test error");
			}
		}
	}
}
