using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleCurd.DataAccess.Interface;
using SimpleCurd.DataAccess.Services;
using SimpleCurd.Model;
using SimpleCurd.Web.Helper;
using SimpleCurd.Web.Models;

namespace SimpleCurd.Web.Controllers
{
    public class EmployeeController : Controller
    {
        protected readonly IEmployeeServices _employeeServices;
        private readonly IRoleServices _roleServices;
        private readonly IDepartmentServices _departmentServices;
        public EmployeeController(IEmployeeServices employeeServices, IRoleServices roleServices, IDepartmentServices departmentServices)
        {
            _employeeServices = employeeServices;
            _roleServices = roleServices;
            _departmentServices = departmentServices;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> View(int id)
        {
            if (id == 0)
            {
                return View(new EmployeeDto());
            }

            var employeEntiy = await _employeeServices.GetEmployeeAsync(id);

            if (employeEntiy == null)
            {
                return RedirectToAction("Index");
            }
            return View(employeEntiy.ToModel());
        }
        [HttpPost]
        public async Task<IActionResult> GetAll([FromBody] Table request)
        {
            var dataList = await _employeeServices.GetAllAsync(request);
            return Json(new
            {
                draw = request.draw,
                recordsTotal = dataList.totalRecords,
                recordsFiltered = dataList.filteredrecords,
                data = dataList.data.ToModelList()
            });
        }
        public async Task<IActionResult> AddOrUpdate(int? id)
        {
            var departmentlist = await _departmentServices.GetAllDepartmentAsync();
            var rolelist = await _roleServices.GetAllActiveRolesAsync();

            ViewBag.roles = new SelectList(rolelist, "Id", "Name");
            ViewBag.department = new SelectList(departmentlist, "Id", "Name");

            if (id == null || id == 0)
            {
                return View(new EmployeeDto());
            }

            var employee = await _employeeServices.GetEmployeeAsync(id.Value);
            if (employee == null)
            {
                return View(new EmployeeDto());
            }

            return View(employee.ToModel());
        }
        [HttpPost]
        public async Task<IActionResult> Inset(EmployeeDto model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Invalid form data." });

            var entity = model.ToEntity();
            if (model.Id == 0)
            {
                var check = await _employeeServices.IsExistByNameAsync(model.FirstName, model.LastName);
                if (check)
                {
                    return Json(new { success = false, message = "Employee already exists." });
                }

                await _employeeServices.CreateEmployeeAsync(entity);
                return Json(new { success = true, message = "Employee added successfully." });
            }
            else
            {
                var existingEmployee = await _employeeServices.GetEmployeeAsync(model.Id);

                if (existingEmployee == null)
                    return Json(new { success = false, message = "Employee not found." });


                existingEmployee.FirstName = model.FirstName;
                existingEmployee.LastName = model.LastName;
                existingEmployee.Email = model.Email;
                existingEmployee.Phone = model.Phone;
                existingEmployee.DateOfBirth = model.DateOfBirth;
                existingEmployee.HireDate = model.HireDate ?? existingEmployee.HireDate;
                existingEmployee.Salary = model.Salary ?? existingEmployee.Salary;
                existingEmployee.DepartmentId = model.DepartmentId;
                existingEmployee.RoleId = model.RoleId;

                // Address update
                existingEmployee.Address.Street = model.Address.Street;
                existingEmployee.Address.City = model.Address.City;
                existingEmployee.Address.State = model.Address.State;
                existingEmployee.Address.PostalCode = model.Address.PostalCode;
                existingEmployee.Address.Country = model.Address.Country;

                await _employeeServices.UpdateEmployeeAsync(existingEmployee);
                return Json(new { success = true, message = "Employee updated successfully." });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return Json(new { success = true, message = "Employee Not Found." });
            }
            var isDeleted = await _employeeServices.DeleteEmployeeAsync(id);

            if (isDeleted)
            {
                return Json(new { success = true, message = "Employee Deleted successfully." });
            }

            return Json(new { success = false, message = "Employee Not Deleted." });
        }
        public async Task<IActionResult> ReActiveRole(int id)
        {
            if (id == 0)
            {
                return Json(new { success = false, message = "Employee Not Found." });
            }
            var employee = await _employeeServices.GetEmployeeAsync(id);
            if (employee == null)
            {
                return Json(new { success = false, message = "Employee Not Found." });
            }
            employee.IsActive = true;
            await _employeeServices.UpdateEmployeeAsync(employee);
            return Json(new { success = true, message = "Employee Actived successfully." });
        }
    }
}
