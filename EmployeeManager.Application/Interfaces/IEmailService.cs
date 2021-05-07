using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManager.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(IEnumerable<string> email, string subject, string message);
    }
}
