using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Application.ViewModels
{
    public class EmailVm
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Position { get; set; }
        public string SelectedOption { get; set; }
    }
}
