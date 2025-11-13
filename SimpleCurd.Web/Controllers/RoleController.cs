using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SimpleCurd.DataAccess.Interface;
using SimpleCurd.Model;
using SimpleCurd.Web.Helper;
using SimpleCurd.Web.Models;
using System.Drawing.Printing;

namespace SimpleCurd.Web.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleServices _rolesServices;
        public RoleController(IRoleServices rolesServices)
        {
            _rolesServices = rolesServices;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> View(int id)
        {
            RoleDto newrole = new RoleDto();
            if (id == 0)
            {
                return View(newrole);
            }

            var role = await _rolesServices.GetRoleAsync(id);

            if (role == null)
            {
                return RedirectToAction("Index");
            }
            return View(role.ToModel());
        }
        [HttpPost]
        public async Task<IActionResult> GetAllRoles([FromBody] Table request)
        {
            var roles = await _rolesServices.GetAllRoleAsync(request);
            return Json(new 
            {
                draw = request.draw,
                recordsTotal = roles.totalRecords,
                recordsFiltered = roles.filteredrecords,
                data = roles.data.ToModelList()
            });
        }
        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(int? id)
        {
            if (id == null || id == 0)
            {
                return View(new RoleDto());
            }

            var role = await _rolesServices.GetRoleAsync(id.Value);
            if (role == null)
            {
                return View(new RoleDto());
            }

            return View(role.ToModel());
        }
        [HttpPost]
        public async Task<IActionResult> Inset(RoleDto model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Invalid form data." });

            var entity = model.ToEntity();
            if (model.Id == 0)
            {
                var existingRole = await _rolesServices.IsExistByNameAsync(model.Name);
                if (existingRole)
                {
                    return Json(new { success = false, message = "Role name already exists." });
                }

                await _rolesServices.CreateRoleAsync(entity);
                return Json(new { success = true, message = "Role added successfully." });
            }
            else
            {
                var existingRole = await _rolesServices.GetRoleAsync(model.Id);

                if (existingRole == null)
                    return Json(new { success = false, message = "Role not found." });


                existingRole.Name = model.Name;
                existingRole.Description = model.Description;

                await _rolesServices.UpdateRoleAsync(existingRole);
                return Json(new { success = true, message = "Role updated successfully." });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return Json(new { success = true, message = "Role Not Found." });
            }
            var isDeleted = await _rolesServices.DeleteRoleAsync(id);

            if (isDeleted)
            {
                return Json(new { success = true, message = "Role Deleted successfully." });
            }

            return Json(new { success = false, message = "Role Not Deleted." });
        }
        public async Task<IActionResult> ReActiveRole(int id)
        {
            if (id == 0)
            {
                return Json(new { success = false, message = "Role Not Found." });
            }
            var role = await _rolesServices.GetRoleAsync(id);
            if (role == null)
            {
                return Json(new { success = false, message = "Role Not Found." });
            }
            role.IsActive = true;
            await _rolesServices.UpdateRoleAsync(role);
            return Json(new { success = true, message = "Role Actived successfully." });
        }
    }
}
