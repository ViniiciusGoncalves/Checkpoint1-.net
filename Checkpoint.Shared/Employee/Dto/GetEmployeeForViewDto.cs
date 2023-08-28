using System;

namespace Checkpoint.Shared.Employee
{
    public class GetEmployeeForViewDto
    {
        public string Name { get; set; }
        public string IdRegistrationCode { get; set; }
        public string Gender { get; set; }
        public decimal ValueHourly { get; set; }
        public decimal Salary { get; set; }
        public bool IsPositionTrust { get; set; }
        public bool IsPjOrClt { get; set; }
        public int ContractedHours { get; set; }
        public string CnpjOrCpf { get; set; }
    }
}
