using System;
using System.Collections.Generic;
using Checkpoint.Shared.Employee;

namespace Checkpoint.Services
{
    public interface IEmployee
    {
        List<GetEmployeeForViewDto> CreateOrEditEmployee(GetEmployeeForViewDto employee);
        bool DeleteEmployee(string idRegistrationCode);
        decimal ComputeTotalCost();
        List<GetEmployeeForViewDto> GetAllEmployees();
        decimal GetAllCostMonthEmployee(string idRegistrationCode);
        void IncreaseSalary(string idRegistrationCode, decimal percentage);
        GetEmployeeForViewDto GetEmployeeById(string idRegistrationCode);
    }
}
