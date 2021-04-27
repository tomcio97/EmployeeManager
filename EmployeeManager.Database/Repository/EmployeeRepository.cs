using System;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.Domain.Interfaces;
using EmployeeManager.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.Database.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        private readonly Context context;

        public EmployeeRepository(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await context.Employees.FirstOrDefaultAsync(e => e.Id == id);
        }

        public IQueryable<Employee> GetEmployees()
        {
          var employees = this.context.Employees;

            Console.WriteLine("dupa");

            return employees;
        }
    }
}
