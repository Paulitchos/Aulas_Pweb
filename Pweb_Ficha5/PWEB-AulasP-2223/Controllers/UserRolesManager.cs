using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
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

            var userRolesViesModel = new List<UserRolesViewModel>();

            foreach(ApplicationUser user in users)
            {
                var utilizadorVM = new UserRolesViewModel();
                utilizadorVM.UserId = user.Id;
                utilizadorVM.PrimeiroNome = user.PrimeiroNome;
                utilizadorVM.UltimoNome = user.UltimoNome;
                utilizadorVM.Roles = (IEnumerable<string>)GetUserRoles(user);

                utilizadorVM
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

            if (user == null)
                return RedirectToAction("Index");

            ViewBag.userId = userId;
            ViewBag.UserName = user.UserName;

            var model = new List<ManageUserRolesViewModel>();
            foreach(var role in _userManager.Roles)
            {
                var userRolesViewModel = new ManageUserRolesViewModel();
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Details(List<ManageUserRolesViewModel> model, string userId)
        {
            /* código a criar */
            return RedirectToAction("Index");
        }
    }
}
