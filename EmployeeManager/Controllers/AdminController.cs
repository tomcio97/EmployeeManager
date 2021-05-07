using System;
using System.Collections.Generic;
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
    public class AdminController : Controller
    {
        private readonly IAdminService adminService;
        private readonly IEmailService emailService;

        public AdminController(IAdminService adminService, IEmailService emailService)
        {
            this.adminService = adminService;
            this.emailService = emailService;
        }
        // GET: AdminController
        public async Task<IActionResult> Index()
        {
            //await emailService.SendEmail(new List<string>() {"twalasik97@gmail.com", "tomasz-walasik@wp.pl" }, "dupa jasia", "dupa jasia hahahaha");

            return View();
        }

        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminController/Create
        public async Task<ActionResult> SendEmail()
        {
            var employeesToEmail = await adminService.GetEmployeesToEmail();
            ViewData["Employees"] = employeesToEmail;

            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendEmail(IFormCollection collection, EmailVm emailVm)
        {
            try
            {
                var employees = await adminService.GetEmployeesToEmail();

                switch (emailVm.SelectedOption)
                {
                    case "all":
                        {
                            await emailService.SendEmail(employees.Select(x => x.Email), emailVm.Subject, emailVm.Message);
                        }
                        break;
                    case "position":
                        await emailService.SendEmail(employees.Where(x => x.Position == emailVm.Position).Select(y => y.Email), emailVm.Subject, emailVm.Message);
                        break;
                    case "employee":
                        await emailService.SendEmail(new List<string> { emailVm.Email }, emailVm.Subject, emailVm.Message);
                        break;
                }


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminController/Edit/5
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

        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminController/Delete/5
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
