using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Domain.Models
{
   public class Employee : BaseModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirdth { get; set; }
        public string Education { get; set; }
        public string Profession { get; set; }
        public string Position { get; set; }
        public float Salary { get; set; }
        public string ProfilePicture { get; set; }
        public List<Address> Addresses { get; set; }

        public Contact Contact { get; set; }
    }
}
