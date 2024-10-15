using Employees.MVC.Data;
using Employees.MVC.Models;
using Employees.MVC.Models.Entities;
using Employees.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Employees.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFileService _fileService;
        private readonly IEmployeeService _employeeService;
        public HomeController(ILogger<HomeController> logger, IFileService fileService, IEmployeeService employeeService)
        {
            _employeeService = employeeService;
            _fileService = fileService;
            _logger = logger;
        }

        public async Task<ActionResult> Index()
        {
            var employees = await _employeeService.GetEmployeesAsync();
            return View(employees);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<ActionResult> Import(IFormFile file)
        {
            var getData = await _fileService.addRecords(file);
            return RedirectToAction("Index");
        }
    }
}
