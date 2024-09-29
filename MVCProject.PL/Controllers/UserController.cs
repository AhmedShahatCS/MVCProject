using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCProject.DAL.Entities;
using MVCProject.PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProject.PL.Controllers
{
	//[Authorize]
	public class UserController : Controller
	{
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _automapper;


        public UserController(UserManager<ApplicationUser> userManager, IMapper automapper)
		{

            _userManager = userManager;
            _automapper = automapper;

        }
        public async Task<IActionResult> Index(string SearcValue)
		{
			if (string.IsNullOrEmpty(SearcValue))
			{
				var users =await _userManager.Users.Select(U => new UserViewModel()
				{
					Id = U.Id,
					Fname = U.FName,
					Lname = U.LName,
					Email = U.Email,
					PhoneNumber=U.PhoneNumber,
					Roles= _userManager.GetRolesAsync(U).Result,

				}).ToListAsync();
				return View(users);
			}
			else
			{
				var user = await _userManager.FindByEmailAsync(SearcValue);
				var MappedUser = new UserViewModel()
				{
					Id = user.Id,
					Fname = user.FName,
					Lname = user.LName,
					Email = user.Email,
					PhoneNumber = user.PhoneNumber,
					Roles = _userManager.GetRolesAsync(user).Result,
				};
				return View(new List<UserViewModel> { MappedUser });

			}
		}


		public async Task<IActionResult> Details(string id,string viewname= "Details")
		{
			if (id == null) return BadRequest();
			var user =await _userManager.FindByIdAsync(id);
			if (user == null) return NotFound();
			var mappedUser = _automapper.Map<ApplicationUser, UserViewModel>(user);

			return View(viewname, mappedUser);



		}
        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");

        }

        [HttpPost]
		public async Task<IActionResult> Edit([FromRoute]string id,UserViewModel model)
		{
			if (id != model.Id) return BadRequest();
			try
			{
				var user = await _userManager.FindByIdAsync(id);
				user.PhoneNumber = model.PhoneNumber;
				user.FName = model.Fname;
				user.LName = model.Lname;
				await _userManager.UpdateAsync(user);
				return RedirectToAction(nameof(Index));

			}catch(Exception ex)
			{
				ModelState.AddModelError(string.Empty, ex.Message);
			}
			return View(model);
			
				
		}


		public async Task<IActionResult> Delete(string id)
		{
            return await Details(id, "Delete");
		
		}
        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
			
			try
			{
				var user = await _userManager.FindByIdAsync(id);
				await _userManager.DeleteAsync(user);
				return RedirectToAction(nameof(Index));


			}
			catch (Exception ex) {
				ModelState.AddModelError(string.Empty, ex.Message);
				return RedirectToAction("Error", "Home");
			}
			



        }
    }
}
