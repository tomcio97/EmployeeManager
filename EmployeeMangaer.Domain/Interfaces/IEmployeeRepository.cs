using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManager.Domain.Models;

namespace EmployeeManager.Domain.Interfaces
{
   public interface IEmployeeRepository : IBaseRepository<Employee>
    {
    }
}
