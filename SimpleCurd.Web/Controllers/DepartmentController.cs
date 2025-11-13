using Microsoft.AspNetCore.Mvc;
using SimpleCurd.DataAccess.Interface;
using SimpleCurd.DataAccess.Services;
using SimpleCurd.Model;
using SimpleCurd.Web.Helper;
using SimpleCurd.Web.Models;

namespace SimpleCurd.Web.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentServices _departmentServices;
        public DepartmentController(IDepartmentServices departmentServices)
        {
            _departmentServices = departmentServices;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> View(int id)
        {
            DepartmentDto newDepartmentDto = new DepartmentDto();
            if (id == 0)
            {
                return View(newDepartmentDto);
            }

            var department = await _departmentServices.GetDepartmentAsync(id);

            if (department == null)
            {
                return RedirectToAction("Index");
            }
            return View(department.ToModel());
        }
        [HttpPost]
        public async Task<IActionResult> GetAll([FromBody] Table request)
        {
            var department = await _departmentServices.GetAllDepartmentAsync(request);
            return Json(new
            {
                draw = request.draw,
                recordsTotal = department.totalRecords,
                recordsFiltered = department.filteredrecords,
                data = department.data.ToModelList()
            });
        }
        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(int? id)
        {
            if (id == null || id == 0)
            {
                return View(new DepartmentDto());
            }

            var department = await _departmentServices.GetDepartmentAsync(id.Value);
            if (department == null)
            {
                return View(new DepartmentDto());
            }

            return View(department.ToModel());
        }
        [HttpPost]
        public async Task<IActionResult> Inset(DepartmentDto model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Invalid form data." });

            var entity = model.ToEntity();
            if (model.Id == 0)
            {
                var existingDepartment = await _departmentServices.IsExistByNameAsync(model.Name);
                if (existingDepartment)
                {
                    return Json(new { success = false, message = "Department already exists." });
                }

                await _departmentServices.CreateDepartmentAsync(entity);
                return Json(new { success = true, message = "Department added successfully." });
            }
            else
            {
                var existingDepartment = await _departmentServices.GetDepartmentAsync(model.Id);

                if (existingDepartment == null)
                    return Json(new { success = false, message = "Department not found." });


                existingDepartment.Name = model.Name;
                existingDepartment.Description = model.Description;

                await _departmentServices.UpdateDepartmentAsync(existingDepartment);
                return Json(new { success = true, message = "Department updated successfully." });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return Json(new { success = true, message = "Department Not Found." });
            }
            var isDeleted = await _departmentServices.DeleteDepartmentAsync(id);

            if (isDeleted)
            {
                return Json(new { success = true, message = "Department Deleted successfully." });
            }

            return Json(new { success = false, message = "Department Not Deleted." });
        }
        public async Task<IActionResult> ReActiveRole(int id)
        {
            if (id == 0)
            {
                return Json(new { success = false, message = "Department Not Found." });
            }
            var role = await _departmentServices.GetDepartmentAsync(id);
            if (role == null)
            {
                return Json(new { success = false, message = "Department Not Found." });
            }
            role.IsActive = true;
            await _departmentServices.UpdateDepartmentAsync(role);
            return Json(new { success = true, message = "Department Actived successfully." });
        }
    }
}
