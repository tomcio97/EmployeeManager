using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EmployeeManager.Application.Interfaces;
using EmployeeManager.Application.ViewModels;
using EmployeeManager.Domain.Interfaces;
using EmployeeManager.Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;


namespace EmployeeManager.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IMapper mapper;
        private readonly IConfigurationProvider configurationProvider;
        private readonly IWebHostEnvironment webHostEnvironment;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper, IConfigurationProvider configurationProvider, IWebHostEnvironment webHostEnvironment)
        {
            this.employeeRepository = employeeRepository;
            this.mapper = mapper;
            this.configurationProvider = configurationProvider;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<int> CreateEmployee(EmployeeVm employeeVm)
        {
            var uniqueFileName = UploadedFile(employeeVm);

            employeeVm.ProfilePicture = uniqueFileName;

            var entityEmployee = mapper.Map<Employee>(employeeVm);

            //entityEmployee.ProfilePicture = uniqueFileName;

            employeeRepository.Add(entityEmployee);
            if (await employeeRepository.SaveAll())
            {
                return entityEmployee.Id;
            }

            return -1;

        }

        public async Task<bool> DeleteEmployee(int id)
        {
            employeeRepository.Delete(await employeeRepository.GetEmployeeById(id));

            return await employeeRepository.SaveAll();
        }

        public async Task<EmployeeVm> GetEmployeeById(int id)
        {
            return mapper.Map<EmployeeVm>(await employeeRepository.GetEmployeeById(id));
        }

        public async Task<List<EmployeeVm>> GetEmployees()
        {

            return await employeeRepository.GetEmployees().ProjectTo<EmployeeVm>(configurationProvider).ToListAsync();
        }

        public async Task<bool> UpdateEmployee(EmployeeVm employeeVm)
        {
            employeeRepository.UpdateEmployee(mapper.Map<Employee>(employeeVm));

            return await employeeRepository.SaveAll();
        }


        private string UploadedFile(EmployeeVm model)
        {
            string uniqueFileName = null;

            if (model.ProfileImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfileImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
