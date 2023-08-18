using Checkpoint.Shared.Employee;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkpoint.Services
{
    public class EmployeeAppService : IEmployee
    {
        private List<GetEmployeeForViewDto> _employeeList = new List<GetEmployeeForViewDto>();

        public List<GetEmployeeForViewDto> CreateOrEditEmployee(GetEmployeeForViewDto employee)
        {
            var existingEmployee = _employeeList.FirstOrDefault(e => e.IdRegistrationCode == employee.IdRegistrationCode);

            if (existingEmployee != null)
            {
                existingEmployee.Name = employee.Name;
                existingEmployee.Gender = employee.Gender;
                existingEmployee.ValueHourly = employee.ValueHourly;
                existingEmployee.IsPositionTrust = employee.IsPositionTrust;
                existingEmployee.IsPjOrCnpj = employee.IsPjOrCnpj;
                existingEmployee.ContractedHours = employee.ContractedHours;
                existingEmployee.CnpjOrCnpj = employee.CnpjOrCnpj;
            }
            else
            {
                _employeeList.Add(employee);
            }

            return _employeeList;
        }

        public bool DeleteEmployee(string idRegistrationCode)
        {
            var employeeToRemove = _employeeList.FirstOrDefault(e => e.IdRegistrationCode == idRegistrationCode);
            if (employeeToRemove != null)
            {
                _employeeList.Remove(employeeToRemove);
                return true;
            }
            return false;
        }

        public decimal ComputeTotalCost()
        {
            throw new NotImplementedException();
        }

        public List<GetEmployeeForViewDto> GetAllEmployees()
        {
            return _employeeList;
        }

        public decimal GetAllCostMonthEmployee(string idRegistrationCode)
        {
            throw new NotImplementedException();
        }

        public void IncreaseSalary(string idRegistrationCode, decimal percentage)
        {
            throw new NotImplementedException();
        }

        public GetEmployeeForViewDto GetEmployeeById(string idRegistrationCode)
        {
            return _employeeList.FirstOrDefault(e => e.IdRegistrationCode == idRegistrationCode);
        }
    }
}
