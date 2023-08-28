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

        public decimal ComputeTotalCost()
        {
            decimal totalCost = 0;

            foreach (var employee in _employeeList)
            {
                decimal monthlyCost = GetAllCostMonthEmployee(employee.IdRegistrationCode);
                totalCost += monthlyCost;
            }

            return totalCost;
        }

        public List<GetEmployeeForViewDto> GetAllEmployees()
        {
            return _employeeList;
        }

        public decimal GetAllCostMonthEmployee(string idRegistrationCode)
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
            return 0;
        }

        public void IncreaseSalary(string idRegistrationCode, decimal increaseAmount, bool isPercentage)
        {
            var employee = _employeeList.FirstOrDefault(e => e.IdRegistrationCode == idRegistrationCode);

            if (employee != null)
            {
                if (employee.IsPjOrClt)
                {
                    if (isPercentage)
                    {
                        // Aumento de salário para funcionário CLT em porcentagem
                        decimal increasePercentage = increaseAmount;
                        decimal increase = employee.Salary * (increasePercentage / 100);
                        employee.Salary += increase;
                        Console.WriteLine($"Salário do funcionário {employee.Name} aumentado em {increasePercentage}% para R$ {employee.Salary}");
                    }
                    else
                    {
                        // Aumento de salário para funcionário CLT em valor fixo (não aplicável)
                        Console.WriteLine("Não é possível aumentar o salário em valor fixo para funcionário CLT.");
                    }
                }
                else
                {
                    if (isPercentage)
                    {
                        // Aumento de salário para funcionário PJ em porcentagem (não aplicável)
                        Console.WriteLine("Não é possível aumentar o salário em porcentagem para funcionário PJ.");
                    }
                    else
                    {
                        // Aumento de salário para funcionário PJ em valor fixo
                        employee.ValueHourly += increaseAmount;
                        Console.WriteLine($"Salário do funcionário {employee.Name} aumentado em R$ {increaseAmount} para R$ {employee.ValueHourly}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Funcionário não encontrado.");
            }
        }


        public GetEmployeeForViewDto GetEmployeeById(string idRegistrationCode)
        {
            return _employeeList.FirstOrDefault(e => e.IdRegistrationCode == idRegistrationCode);
        }
    }
}
