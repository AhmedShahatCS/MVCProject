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
		private readonly SignInManager<ApplicationUser> _signInManger;

		public AccountController(UserManager<ApplicationUser> usermanger,SignInManager<ApplicationUser> signInManger)
        {
		    _usermanger = usermanger;
			_signInManger = signInManger;
		}
        #region SignIn
        [HttpGet]
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
        #endregion

        #region Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _usermanger.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var res = await _usermanger.CheckPasswordAsync(user, model.Password);
                    if (res)
                    {
                        var R = await _signInManger.PasswordSignInAsync(user, model.Password, model.RemenberMe, false);
                        if (R.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");

                        }

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "InVaild Login");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "InVaild Login");
                }

            }
            return View(model);
        } 
        #endregion

        public new async Task<IActionResult> SignOut()
        {
            await _signInManger.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
