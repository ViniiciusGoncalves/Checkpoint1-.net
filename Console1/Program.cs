﻿using Checkpoint.Services;
using Checkpoint.Shared.Employee;
using System;
using System.Collections.Generic;

namespace Console1
{
    class Program
    {
        static void Main(string[] args)
        {
            EmployeeAppService employeeAppService = new EmployeeAppService();

            while (true)
            {
                Console.WriteLine("Escolha uma opção:");
                Console.WriteLine("1 - Criar ou editar funcionário");
                Console.WriteLine("2 - Listar todos os funcionários");
                Console.WriteLine("3 - Sair");
                Console.Write("Opção: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Ele é pj ou clt (true/false): "); //TRUE == CLT FALSE == PJ
                        bool isPjOrClt = bool.Parse(Console.ReadLine());
                        Console.Write("Digite o código de registro do funcionário: ");
                        string registrationCode = Console.ReadLine();
                        Console.Write("Digite o nome do funcionário: ");
                        string name = Console.ReadLine();
                        Console.Write("Digite o gênero do funcionário: ");
                        string gender = Console.ReadLine();

                        GetEmployeeForViewDto newEmployee = new GetEmployeeForViewDto
                        {
                            IdRegistrationCode = registrationCode,
                            Name = name,
                            Gender = gender,
                            IsPjOrClt = isPjOrClt,
                        };

                        if (isPjOrClt)
                        {   //FUNCIONARIO CLT
                            Console.Write("Ele tem um cargo de confiança?: "); //TRUE == SIM, FALSE == NÃO
                            bool isPositionTrust = bool.Parse(Console.ReadLine());
                            Console.Write("Digite o cpf dele: ");
                            string cnpjOrCnpj = Console.ReadLine();
                            Console.Write("Digite o salario do funcionario: ");
                            decimal salary = decimal.Parse(Console.ReadLine());

                            newEmployee.IsPositionTrust = isPositionTrust;
                            newEmployee.CnpjOrCpf = cnpjOrCnpj;
                            newEmployee.Salary = salary;
                        }
                        else
                        {   //FUNCIONARIO PJ
                            Console.Write("Digite as horas contratadas: ");
                            int contractedHours = int.Parse(Console.ReadLine());
                            Console.Write("Digite o cnpj dele: ");
                            string cpf = Console.ReadLine();
                            Console.Write("Digite o valor de horas do funcionário: ");
                            float valueHourly = float.Parse(Console.ReadLine());

                            newEmployee.ContractedHours = contractedHours;
                            newEmployee.CnpjOrCpf = cpf;
                            newEmployee.ValueHourly = valueHourly;
                        }

                        employeeAppService.CreateOrEditEmployee(newEmployee);
                        Console.WriteLine("Funcionário criado ou editado com sucesso.");
                        break;

                    case "2":
                        List<GetEmployeeForViewDto> allEmployees = employeeAppService.GetAllEmployees();
                        Console.WriteLine("\nLista de Funcionários:");
                        foreach (var employee in allEmployees)
                        {
                            Console.WriteLine($"Nome: {employee.Name}, Código de Registro: {employee.IdRegistrationCode}, Gênero: {employee.Gender}, Horas trabalhadas: {employee.ValueHourly}");

                            decimal monthlyCost = employeeAppService.ComputeTotalCost(employee.IdRegistrationCode);
                            Console.WriteLine($"Custo Mensal: R$ {monthlyCost}");
                        }
                        break;

                    case "3":
                        Console.WriteLine("Encerrando o programa.");
                        return;

                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }

                Console.WriteLine();
            }
        }
    }
}
