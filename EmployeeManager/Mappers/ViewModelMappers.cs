using System;
using AutoMapper;
using EmployeeManager.Application.ViewModels;
using EmployeeManager.Domain.Models;

namespace EmployeeManager.Application.Mappers
{

    public class ViewModelMappers : Profile
    {
        public ViewModelMappers()
        {
            CreateMap<EmployeeVm, Employee>().ForMember(x => x.Contact, y => y.Ignore()).ReverseMap().ForMember(x => x.Age, y => y.MapFrom(z => 
            z.DateOfBirdth.AddYears(DateTime.Today.Year - z.DateOfBirdth.Year) <= DateTime.Today ? DateTime.Today.Year - z.DateOfBirdth.Year : (DateTime.Today.Year - z.DateOfBirdth.Year)-1));
            CreateMap<ContactVm, Contact>().ReverseMap();
            CreateMap<AddressVm, Address>().ReverseMap();
        }
    }
}
