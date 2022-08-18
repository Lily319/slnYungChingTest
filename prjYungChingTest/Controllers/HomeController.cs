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
       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
