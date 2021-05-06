using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManager.Application.ViewModels;
using EmployeeManager.Domain.Models;

namespace EmployeeManager.Application.Interfaces
{
   public interface IEmployeeService
    {
        Task<int> CreateEmployee(EmployeeVm employeeVm);

        Task<List<EmployeeVm>> GetEmployees(string searchString, string sortOrder);

        Task<EmployeeVm> GetEmployeeById(int id);

        Task<bool> UpdateEmployee(EmployeeVm employeeVm);

        Task<bool> DeleteEmployee(int id);
    }
}
