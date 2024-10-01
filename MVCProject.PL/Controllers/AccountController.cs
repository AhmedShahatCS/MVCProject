 using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCProject.DAL.Entities;
using MVCProject.PL.Helpers;
using MVCProject.PL.ViewModels;
using System;
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
        #region Registor
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

        #region SignOut
        public new async Task<IActionResult> SignOut()
        {
            await _signInManger.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        #endregion

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user =await _usermanger.FindByEmailAsync(model.Email);
                if(user is not null)
                {
                    //send email 
                    var token=await _usermanger.GeneratePasswordResetTokenAsync(user);
                    var url = Url.Action("ResetPassword", "Account", new { email = model.Email, Token = token }, Request.Scheme);

                    var email = new Email()
                    {
                        Subject = "Reset Password",
                        To = model.Email,
                        body = url
                    };
                    EmailSettings.SendEmail(email);
                    return RedirectToAction(nameof(CheckYourInbox));

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invaild Email");
                }

            }
            
            
                return View("ForgetPassword", model);
            

        }
        public IActionResult CheckYourInbox()
        {
            return View();
        }
        public IActionResult ResetPassword(string email,string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;

            return View();

        }
        [HttpPost]

        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                string email = TempData["email"] as String;
                string token = TempData["token"] as String;

                var user =await _usermanger.FindByEmailAsync(email);
               var result= await _usermanger.ResetPasswordAsync(user, token, model.NewPassword);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    foreach( var err in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, err.Description);
                    }
                }


            }
            return View(model);
        }
    }
}
