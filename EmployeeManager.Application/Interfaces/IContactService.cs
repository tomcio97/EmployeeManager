using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManager.Application.ViewModels;

namespace EmployeeManager.Application.Interfaces
{
   public interface IContactService
    {
        Task<int> CreateContact(ContactVm contact, int EmployeeId);
    }
}
