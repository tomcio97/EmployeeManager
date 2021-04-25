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
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository addressRepository;
        private readonly IMapper mapper;

        public AddressService(IAddressRepository addressRepository, IMapper mapper)
        {
            this.addressRepository = addressRepository;
            this.mapper = mapper;
        }

        public async Task<int> CreateAddress(AddressVm address, int employeeId)
        {
            var entityAddress = mapper.Map<Address>(address);
            entityAddress.EmployeeId = employeeId;

            addressRepository.Add(entityAddress);
            if(await addressRepository.SaveAll())
            {
                return entityAddress.Id;
            }

            return -1;
        }
    }
}
