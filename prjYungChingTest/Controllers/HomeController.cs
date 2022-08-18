using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using prjYungChingTest.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace prjYungChingTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private NorthwindContext db;

        public HomeController(ILogger<HomeController> logger, NorthwindContext context)
        {
            _logger = logger;
            db = context;
        }

        public IActionResult Employees()
        {
            var e = db.Employees;
            return View(e);
        }
        [HttpPost]
        public IActionResult Employees(string keyword)
        {
            if (String.IsNullOrEmpty(keyword))
            {
                var e = db.Employees;
                return View(e);
            }
            else
            {
                var e = db.Employees.Where(e => e.FirstName.Contains(keyword) || e.LastName.Contains(keyword));
                return View(e);
            }
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            db.Employees.Add(employee);
            db.SaveChanges();
            return RedirectToAction("Employees", "Home");
        }
        public IActionResult Edit(int? id)
        {
            var e = db.Employees.FirstOrDefault(e => e.EmployeeId == id);
            return View(e);
        }
        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            var e = db.Employees.FirstOrDefault(e => e.EmployeeId == employee.EmployeeId);
            e.LastName = employee.LastName;
            e.FirstName = employee.FirstName;
            e.Title = employee.Title;
            e.TitleOfCourtesy = employee.TitleOfCourtesy;
            e.BirthDate = employee.BirthDate;
            e.HireDate = employee.HireDate;
            e.Address = employee.Address;
            e.City = employee.City;
            e.Region = employee.Region;
            e.PostalCode = employee.PostalCode;
            e.Country = employee.Country;
            e.HomePhone = employee.HomePhone;
            e.Extension = employee.Extension;
            e.Notes = employee.Notes;
            e.ReportsTo = employee.ReportsTo;
            db.SaveChanges();
            return RedirectToAction("Edit", "Home", employee.EmployeeId);
        }
        public IActionResult Delete(int? id)
        {
            try
            {
                var e = db.Employees.FirstOrDefault(e => e.EmployeeId == id);
                db.Remove(e);
                db.SaveChanges();
                return RedirectToAction("Employees", "Home");
            }
            catch
            {
                return RedirectToAction("Employees", "Home");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
