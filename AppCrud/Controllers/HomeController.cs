using AppCrud.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using AppCrud.Repositories.Contract;

namespace AppCrud.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IGenericRepository<Department> _departmentRepository;
        private readonly IGenericRepository<Employee> _employeeRepository;
        public HomeController(
            ILogger<HomeController> logger,
            IGenericRepository<Department> departmentRepository,
            IGenericRepository<Employee> employeeRepository
            )
        {
            _logger = logger;
            _departmentRepository = departmentRepository;
            _employeeRepository = employeeRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async  Task<IActionResult> GetDepartmentList()
        {
            List<Department> _departmentList = await _departmentRepository.GetList();
            return StatusCode(StatusCodes.Status200OK, _departmentList);
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployeeList()
        {
            List<Employee> _employeeList = await _employeeRepository.GetList();
            return StatusCode(StatusCodes.Status200OK, _employeeList);
        }

        [HttpPost]
        public async Task<IActionResult> SaveEmployee([FromBody] Employee model)
        {
            bool _result = await _employeeRepository.Save(model);
            if(_result)
                return StatusCode(StatusCodes.Status200OK, new { value = _result, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { value = _result, msg = "error" });
        }

        [HttpPut]
        public async Task<IActionResult> EditEmployee([FromBody] Employee model)
        {
            bool _result = await _employeeRepository.Edit(model);
            if (_result)
                return StatusCode(StatusCodes.Status200OK, new { value = _result, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { value = _result, msg = "error" });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(int idEmployee)
        {
            bool _result = await _employeeRepository.Delete(idEmployee);
            if (_result)
                return StatusCode(StatusCodes.Status200OK, new { value = _result, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { value = _result, msg = "error" });
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
    }
}