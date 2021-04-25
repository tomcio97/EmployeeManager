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
    public class ContactService : IContactService
    {
        private readonly IMapper mapper;
        private readonly IContactRepository contactRepository;

        public ContactService(IMapper mapper, IContactRepository contactRepository)
        {
            this.mapper = mapper;
            this.contactRepository = contactRepository;
        }

        public async Task<int> CreateContact(ContactVm contact, int employeeId)
        {
            var entityContact = mapper.Map<Contact>(contact);
            entityContact.EmployeeId = employeeId;

            contactRepository.Add(entityContact);
            if(await contactRepository.SaveAll())
            {
                return entityContact.Id;
            }

            return -1;
        }
    }
}
