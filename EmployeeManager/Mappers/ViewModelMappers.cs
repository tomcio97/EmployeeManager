using AutoMapper;
using EmployeeManager.Application.ViewModels;
using EmployeeManager.Domain.Models;

namespace EmployeeManager.Application.Mappers
{

    public class ViewModelMappers : Profile
    {
        public ViewModelMappers()
        {
            CreateMap<EmployeeVm, Employee>().ForMember(x => x.Contact, y => y.Ignore()).ReverseMap();
            CreateMap<ContactVm, Contact>().ReverseMap();
            CreateMap<AddressVm, Address>().ReverseMap();
        }
    }
}
