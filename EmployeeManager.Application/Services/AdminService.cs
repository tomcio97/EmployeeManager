using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EmployeeManager.Application.Interfaces;
using EmployeeManager.Application.ViewModels;
using EmployeeManager.Database.Repository;
using EmployeeManager.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IConfigurationProvider configurationProvider;

        public AdminService(IEmployeeRepository employeeRepository, IConfigurationProvider configurationProvider)
        {
            this.employeeRepository = employeeRepository;
            this.configurationProvider = configurationProvider;
        }

        public async Task<List<EmployeeToEmailVm>> GetEmployeesToEmail()
        {
           return await employeeRepository.GetEmployees().ProjectTo<EmployeeToEmailVm>(configurationProvider).ToListAsync();
        }
    }
}
