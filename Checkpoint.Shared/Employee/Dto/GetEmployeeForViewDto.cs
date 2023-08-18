﻿using System;

namespace Checkpoint.Shared.Employee
{
    public class GetEmployeeForViewDto
    {
        public string Name { get; set; }
        public string IdRegistrationCode { get; set; }
        public string Gender { get; set; }
        public float ValueHourly { get; set; }
        public bool IsPositionTrust { get; set; }
        public bool IsPjOrCnpj { get; set; }
        public DateTime ContractedHours { get; set; }
        public string CnpjOrCnpj { get; set; }
    }
}