using System;
using System.Linq;
using AutoMapper;
using EmployeeManager.Application.ViewModels;
using EmployeeManager.Domain.Models;

namespace EmployeeManager.Application.Mappers
{

    public class ViewModelMappers : Profile
    {
        public ViewModelMappers()
        {
            CreateMap<EmployeeVm, Employee>().ReverseMap().ForMember(x => x.Age, y => y.MapFrom(z => 
            z.DateOfBirdth.AddYears(DateTime.Today.Year - z.DateOfBirdth.Year) <= DateTime.Today ? DateTime.Today.Year - z.DateOfBirdth.Year : (DateTime.Today.Year - z.DateOfBirdth.Year)-1));
            CreateMap<ContactVm, Contact>().ReverseMap();
            CreateMap<AddressVm, Address>().ReverseMap();
            CreateMap<Employee, EmployeeToEmailVm>().ForMember(x => x.Email, y => y.MapFrom(z => z.Contact.email));
        }
    }
}
