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
                existingEmployee.IsPjOrClt = employee.IsPjOrClt;
                existingEmployee.ContractedHours = employee.ContractedHours;
                existingEmployee.CnpjOrCpf = employee.CnpjOrCpf;
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

        public decimal ComputeTotalCost(string idRegistrationCode)
        {
            var employee = _employeeList.FirstOrDefault(e => e.IdRegistrationCode == idRegistrationCode);

            if (employee != null)
            {
                if (employee.IsPjOrClt == false)
                {
                    // Cálculo do custo para funcionário PJ (valor hora vezes quantidade de horas contratada)
                    decimal monthlyCostPj = (decimal)employee.ValueHourly * employee.ContractedHours;
                    return monthlyCostPj;
                }
                else
                {
                    // Cálculo do custo para funcionário CLT
                    decimal monthlyCostClt = employee.Salary +
                        (employee.Salary * 0.1111m) + // Fração de férias
                        (employee.Salary * 0.0833m) + // Fração de 13º salário
                        (employee.Salary * 0.08m) +   // FGTS
                        (employee.Salary * 0.04m) +   // Provisão de multa para rescisão
                        (employee.Salary * 0.0793m);  // Previdenciário
                    return monthlyCostClt;
                }
            }

            // Retornar 0 ou algum valor padrão se o funcionário não for encontrado
            return 0;
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
