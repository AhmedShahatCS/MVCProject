using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCProject.DAL.Entities;
using MVCProject.PL.ViewModels;
using System.Threading.Tasks;

namespace MVCProject.PL.Controllers
{
    public class AccountController: Controller
    {
		private readonly UserManager<ApplicationUser> _usermanger;

		public AccountController(UserManager<ApplicationUser> usermanger)
        {
		    _usermanger = usermanger;
		}
        public IActionResult Registor()
        {
            return View();
        }
        [HttpPost]
		public async Task<IActionResult> Registor(RegistorViewModel model)
		{
            if (ModelState.IsValid)
            {

                var user = new ApplicationUser()
                {
                    UserName = model.Email.Split('@')[0],
                    Email = model.Email,
                    FName = model.FName,
                    LName = model.LName,
                    IsAgree = model.IsAgree,
                };
                var result = await _usermanger.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login");


                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);

                    }
                }
            }
			return View(model);
		}
	}
}
