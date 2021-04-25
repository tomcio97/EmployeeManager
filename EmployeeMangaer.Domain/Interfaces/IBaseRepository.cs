using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManager.Domain.Models;

namespace EmployeeManager.Domain.Interfaces
{
    public interface IBaseRepository<Entity> where Entity : BaseModel
    {
        int Add(Entity entity);

        int Delete(Entity entity);

        Task<bool> SaveAll();

    }
}
