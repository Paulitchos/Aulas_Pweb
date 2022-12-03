using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PWEB_AulasP_2223.Data;

namespace PWEB_AulasP_2223.Controllers
{
    public class RoleManagerController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleManagerController(RoleManager<IdentityRole> roleManager)
        {
            this._roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string Id)
        {
            var role = _roleManager.Roles.Where(r => r.Id == Id).FirstOrDefault();

            if(role != null)
            {
                await _roleManager.DeleteAsync(role);
            }

            return RedirectToAction("Index");
        }
    }
}
