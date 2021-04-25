using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManager.Application.Interfaces;
using EmployeeManager.Application.ViewModels;
using EmployeeManager.Domain.Interfaces;
using EmployeeManager.Domain.Models;

namespace EmployeeManager.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IMapper mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            this.employeeRepository = employeeRepository;
            this.mapper = mapper;
        }

        public async Task<int> CreateEmployee(EmployeeVm employeeVm)
        {
            var entityEmployee = mapper.Map<Employee>(employeeVm);

            employeeRepository.Add(entityEmployee);
            if (await employeeRepository.SaveAll())
            {
                return entityEmployee.Id;
            }

            return -1;

        }
    }
}
