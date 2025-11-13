using Microsoft.AspNetCore.Mvc;
using SimpleCurd.DataAccess.Interface;
using System.Threading.Tasks;

namespace SimpleCurd.Web.Controllers;

public class HomeController : Controller
{
    private readonly IEmployeeServices _employeeServices;
    private readonly IDepartmentServices _departmentServices;
    private readonly IRoleServices _roleServices;
    public HomeController(IEmployeeServices employeeServices, IDepartmentServices departmentServices, IRoleServices roleServices)
    {
        _employeeServices = employeeServices;
        _departmentServices = departmentServices;
        _roleServices = roleServices;
    }

    public async Task<IActionResult> Index()
    {  
        int employeeCount = await _employeeServices.Count();
        int departmentCount = await _departmentServices.Count();
        int roleCount = await _roleServices.Count();

        int employeeActiveCount = await _employeeServices.Count(isActive: true);
        int departmentActiveCount = await _departmentServices.Count(isActive: true);
        int roleActiveCount = await _roleServices.Count(isActive: true);

        ViewBag.EmployeeCount = employeeCount;
        ViewBag.DepartmentCount = departmentCount;
        ViewBag.RoleCount = roleCount;

        ViewBag.EmployeeActiveCount = employeeActiveCount;
        ViewBag.DepartmentActiveCount = departmentActiveCount;
        ViewBag.RoleActiveCount = roleActiveCount;


        return View();
    }
}
