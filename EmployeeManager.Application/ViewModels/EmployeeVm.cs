using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace EmployeeManager.Application.ViewModels
{
    public class EmployeeVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirdth { get; set; }
        public int Age { get; set; }
        public string Education { get; set; }
        public string Profession { get; set; }
        public string Position { get; set; }
        public string ProfilePicture { get; set; }
        public float Salary { get; set; }
        public IFormFile ProfileImage { get; set; }
        public AddressVm Address { get; set; }
        public List<AddressVm> Addresses { get; set; }
        public ContactVm Contact { get; set; }
        public string SearchString { get; set; }

    }

}
