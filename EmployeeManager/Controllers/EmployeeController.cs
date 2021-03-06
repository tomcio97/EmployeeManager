using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.Application.Interfaces;
using EmployeeManager.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManager.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly IContactService contactService;
        private readonly IAddressService addressService;

        public EmployeeController(IEmployeeService employeeService, IContactService contactService, IAddressService addressService) 
        {
            this.employeeService = employeeService;
            this.contactService = contactService;
            this.addressService = addressService;
        }
        // GET: EmployeeController
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            ViewData["NameSortParm"] = sortOrder == "Name" ? "name_desc" : "Name";
            ViewData["CitySortParm"] = sortOrder == "City" ? "city_desc" : "City";
            ViewData["AgeSortParm"] = sortOrder == "Age" ? "age_desc" : "Age";
            ViewData["PositionSortParm"] = sortOrder == "Position" ? "position_desc" : "Position";
            ViewData["SalarySortParm"] = sortOrder == "Salary" ? "salary_desc" : "Salary";
            ViewData["CurrentFilter"] = searchString;

            var employees = await employeeService.GetEmployees(searchString, sortOrder);

            return View(employees);
        }

        // GET: EmployeeController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var employee = await employeeService.GetEmployeeById(id);
            return View(employee);
        }

        // GET: EmployeeController/Create
        public ActionResult CreateEmployee()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateEmployee(IFormCollection collection, EmployeeVm employee)
        {
            try
            {

                var employeeId = await employeeService.CreateEmployee(employee);

                if (employeeId != -1)
                {
                    await contactService.CreateContact(employee.Contact, employeeId);
                    await addressService.CreateAddress(employee.Address, employeeId);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

 

        // GET: EmployeeController/Edit/5
        public async Task<ActionResult> EditEmployee(int id)
        {
            return View(await employeeService.GetEmployeeById(id));
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditEmployee(int id, int contactId, int addressId, IFormCollection collection, EmployeeVm employee)
        {
            try
            {
                employee.Id = id;
                employee.Addresses[0].Id = addressId;
                employee.Contact.Id = contactId;


                await employeeService.UpdateEmployee(employee);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteEmployee(int id, IFormCollection collection)
        {
            try
            {
                await employeeService.DeleteEmployee(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
