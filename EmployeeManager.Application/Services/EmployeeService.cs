using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EmployeeManager.Application.Interfaces;
using EmployeeManager.Application.ViewModels;
using EmployeeManager.Domain.Interfaces;
using EmployeeManager.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IMapper mapper;
        private readonly IConfigurationProvider configurationProvider;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper, IConfigurationProvider configurationProvider)
        {
            this.employeeRepository = employeeRepository;
            this.mapper = mapper;
            this.configurationProvider = configurationProvider;
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

        public async Task<EmployeeVm> GetEmployeeById(int id)
        {
            return  mapper.Map<EmployeeVm>(await employeeRepository.GetEmployeeById(id));
        }

        public async Task<List<EmployeeVm>> GetEmployees()
        {

            return await employeeRepository.GetEmployees().ProjectTo<EmployeeVm>(configurationProvider).ToListAsync();
        }
    }
}
