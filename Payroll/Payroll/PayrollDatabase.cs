using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    public interface PayrollDatabase
    {
        void AddEmployee(Employee employee);//添加员工
		Employee GetEmployee(int id);//获取员工
		void DeleteEmployee(int id);//删除员工
		void AddUnionMember(int id, Employee e);//员工加会
		Employee GetUnionMember(int id);//获取协会成员
		void RemoveUnionMember(int memberId);//退出协会
		ArrayList GetAllEmployeeIds();
		IList GetAllEmployees();
	}
}
