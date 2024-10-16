using Employees.Application.Responses;
using Employees.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Employees.Web.Controllers
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
            if (file == null || file.Length == 0)
            {
                TempData["Message"] = "No file selected or the file is empty.";
                TempData["IsSuccess"] = false;
                return RedirectToAction("Index");
            }

            var response = await _fileService.addRecords(file);

            if (response.IsSuccess)
            {
                TempData["Message"] = $"Import successful. Total items: {response.Data.TotalItems}, Added: {response.Data.Q_Added}, Failure: {response.Data.Q_Failure}.";
                TempData["IsSuccess"] = true;

                if (response.Data.Errors != null && response.Data.Errors.Any())
                {
                    TempData["Errors"] = JsonConvert.SerializeObject(response.Data.Errors);
                }
            }
            else
            {
                TempData["Message"] = "Import failed. " + response.Message;
                TempData["IsSuccess"] = false;
            }

            return RedirectToAction("Index");
        }
    }
}
