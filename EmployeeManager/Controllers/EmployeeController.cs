using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.Application.Interfaces;
using EmployeeManager.Application.ViewModels;
using EmployeeManager.Database;
using EmployeeManager.Database.Repository;
using EmployeeManager.Domain.Models;
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
        public ActionResult Index()
        {
            return View();
        }

        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
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
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
