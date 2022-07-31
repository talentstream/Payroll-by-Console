namespace Payroll
{
    public class Program
    {
        static void Main(string[] args)
        {
            PayrollTest test =  new PayrollTest();
            test.SetUp();
            test.TestAddSalariedEmployee();

            test.TestAddHourlyEmployee();

            test.TestAddCommissionedEmployee();

            test.TestDeleteEmplyee();

            test.TestTimeCardTransaction();

            test.TestSalesReceiptTransaction();

            test.TestAddServiceChargeTransaction();

            test.TestChangeNameTransaction();

            test.TestChangeHourlyTransaction();

            test.TestChangeSalaryTransaction();

            test.TestChangeCommisionTransaction();

            test.ChangeDirectDepositMethod();

            test.ChangeHoldMethod();

            test.ChangeMailMethod();

            test.ChangeUnionMember();

            test.ChangeUnaffiliatedMember();

            test.PaySingleSalariedEmployee();

            test.PaySingleSalariedEmployeeOnWrongDate();

            test.PayingSingleHourlyEmployeeNoTimeCards();

            test.PaySingleHourlyEmployeeOneTimeCard();

            test.PaySingleHourlyEmployeeOvertimeOneTimeCard();

            test.PaySingleHourlyEmployeeOnWrongDate();

            test.PaySingleHourlyEmployeeTwoTimeCards();

            test.TestPaySingleHourlyEmployeeWithTimeCardsSpanningTwoPayPeriods();

            test.PayingSingleCommissionedEmployeeNoReceipts();

            test.PaySingleCommissionedEmployeeOneReceipt();

            test.PaySingleCommissionedEmployeeOnWrongDate();

            test.PaySingleCommissionedEmployeeTwoReceipts();

            test.TestPaySingleCommissionedEmployeeWithReceiptsSpanningTwoPayPeriods();

            test.SalariedUnionMemberDues();

            test.HourlyUnionMemberServiceCharge();

            test.ServiceChargesSpanningMultiplePayPeriods();
        }
    }
}