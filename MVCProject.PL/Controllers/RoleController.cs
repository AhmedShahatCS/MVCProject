using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCProject.PL.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;

namespace MVCProject.PL.Controllers
{
    public class RoleController : Controller
    {


        private readonly RoleManager<IdentityRole> _rolemanger;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<IdentityRole> rolemanger, IMapper mapper)
        {
            _rolemanger = rolemanger;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string searchvalue)
        {
            if (string.IsNullOrEmpty(searchvalue))
            {
                var Roles = await _rolemanger.Roles.ToListAsync();
                var r = _mapper.Map<IEnumerable<IdentityRole>, IEnumerable<RoleViewModel>>(Roles);
                return View(r);


            }
            else
            {
                var role = await _rolemanger.FindByNameAsync(searchvalue);
                var R = _mapper.Map<IdentityRole, RoleViewModel>(role);
                return View(new List<RoleViewModel>() { R });
            }

        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = _mapper.Map<RoleViewModel, IdentityRole>(model);
                var r = await _rolemanger.CreateAsync(role);
                return RedirectToAction(nameof(Index));

            }
            return View(model);
        }


        public async Task<IActionResult> Details(string id, string viewname = "Details")
        {
            if (id == null) return BadRequest();
            var role = await _rolemanger.FindByIdAsync(id);
            if (role == null) return NotFound();
            var mappedUser = _mapper.Map<IdentityRole, RoleViewModel>(role);
            return View(viewname, mappedUser);



        }

        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");

        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] string id, RoleViewModel model)
        {
            if (id != model.Id) return BadRequest();
            try
            {
                var role = await _rolemanger.FindByIdAsync(id);
                role.Name = model.RoleName;

                await _rolemanger.UpdateAsync(role);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
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
                var user = await _rolemanger.FindByIdAsync(id);
                await _rolemanger.DeleteAsync(user);
                return RedirectToAction(nameof(Index));


            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Error", "Home");
            }




        }
    }
}
