using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PWEB_AulasP_2223.Models;
using PWEB_AulasP_2223.ViewModels;

namespace PWEB_AulasP_2223.Controllers
{
    public class UserRolesManagerController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserRolesManagerController(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;    
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();

            var userRolesViewModel = new List<UserRolesViewModel>();

            foreach(ApplicationUser u in users)
            {
                var utilizadorVM = new UserRolesViewModel();
                utilizadorVM.UserId = u.Id;
                utilizadorVM.PrimeiroNome = u.PrimeiroNome;
                utilizadorVM.UltimoNome = u.UltimoNome;
                utilizadorVM.Avatar = u.Avatar;
                utilizadorVM.Roles = await GetUserRoles(u);
                userRolesViewModel.Add(utilizadorVM);
            }

            return View(userRolesViewModel);
        }
        private async Task<List<string>> GetUserRoles(ApplicationUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }
        public async Task<IActionResult> Details(string userId)
        {

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return RedirectToAction("Index");

            ViewBag.UserId = userId;
            ViewBag.UserName = user.UserName;
            var listaDeRoles = new List<ManageUserRolesViewModel>();
            foreach (var role in _roleManager.Roles)
            {
                var roleVMToAdd = new ManageUserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                };
                roleVMToAdd.Selected = await _userManager.IsInRoleAsync(user, role.Name);
                listaDeRoles.Add(roleVMToAdd);
            }

            return View(listaDeRoles);
        }
        [HttpPost]
        public async Task<IActionResult> Details(List<ManageUserRolesViewModel> model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }
            result = await _userManager.AddToRolesAsync(user, model.Where(x => x.Selected).Select(y => y.RoleName));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }

            return RedirectToAction("Index");
        }
    }
}
