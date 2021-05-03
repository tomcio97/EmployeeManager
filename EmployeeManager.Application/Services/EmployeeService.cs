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
            var employeeEntity = await employeeRepository.GetEmployeeById(id);
            employeeRepository.Delete(employeeEntity);

             if(await employeeRepository.SaveAll())
            {
                DeleteFile(employeeEntity.ProfilePicture);
                return true;
            }

            return false;
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
            var employee = await employeeRepository.GetEmployeeById(employeeVm.Id);
            if (employeeVm.ProfileImage == null)
            {
                employeeVm.ProfilePicture = employee.ProfilePicture;
            } else
            {
                DeleteFile(employee.ProfilePicture);
                var uniqueFileName = UploadedFile(employeeVm);

                employeeVm.ProfilePicture = uniqueFileName;
            }

            mapper.Map(employeeVm, employee);

            return await employeeRepository.SaveAll();
        }

        #region FileOperation
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

        private void DeleteFile(string fileName)
        {
            if(!String.IsNullOrEmpty(fileName))
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                string filePath = Path.Combine(uploadsFolder, fileName);
                if(File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

            }
        }

        #endregion
    }
}
