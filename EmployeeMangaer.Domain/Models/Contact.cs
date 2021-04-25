using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Domain.Models
{
    public class Contact: BaseModel
    {
        public int telephoneNumber { get; set; }
        public string email { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
