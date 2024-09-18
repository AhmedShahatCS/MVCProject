using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MVCProject.BLL.Interfaces;
using MVCProject.DAL.Entities;
using MVCProject.PL.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace MVCProject.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository db;
        private readonly IDepartmentRepository _deptRepo;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository Db,IDepartmentRepository DeptRepo,IMapper mapper)
        {
            db = Db;
            _deptRepo = DeptRepo;
            _mapper = mapper;
        }
        public IActionResult Index( string valueSearch)
        {

            //viewData it is a Dictionary obj int .net framework 3.5 
            // that used to transfer data from action to view
            //it is va KeyValuePair

            //ViewData["message"] = "Hello from View Data";

            //viewBag it is a  dynamic property int .net framework 4.0
            // that used to transfer data from action to view
            //it is  a property
            ViewBag.Message = "Hello from View Bag";
            IEnumerable<Employee> emp;
            if (string.IsNullOrEmpty(valueSearch))
            {
                emp = db.GetAll();
            }
            else
            {
                emp = db.GetByName(valueSearch);
            }

             
            var MappedEmp = _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeViewModel>>(emp);
            return View(MappedEmp);
        }

        public IActionResult Create()
        {
            ViewBag.depts = _deptRepo.GetAll();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel emp)
        {
            if (ModelState.IsValid)
            {
                var MappedEmp = _mapper.Map<EmployeeViewModel, Employee>(emp);
                var count=db.Add(MappedEmp);
                if (count > 0)
                    TempData["Message"] = "Employee Is Created";
                {
                    return RedirectToAction(nameof(Index));
                }
                
            }
            return View(emp);
        }

        public IActionResult Details(int? id,string view="Details")
        {
            if (id is null) return BadRequest();
            var res = db.Get(id.Value);
            if (res is null) return NotFound();
            var MappedEmp = _mapper.Map<Employee, EmployeeViewModel>(res);
            return View(MappedEmp);


        }
        [HttpGet]
        public IActionResult Edit(int? id) {
            ViewBag.depts = _deptRepo.GetAll();

            return Details(id, "Edit");
        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EmployeeViewModel emp,[FromRoute]int id)
        {

            if (id != emp.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(emp);

                    db.Update(mappedEmp);
                    return RedirectToAction(nameof(Index));
                }catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(emp);

        }

        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(EmployeeViewModel emp, [FromRoute] int id)
        {

            if (id != emp.Id) return BadRequest();
            try { 
                    var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(emp);

                db.Delete(mappedEmp);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                return View(emp);

                }
        }

        
    }
}
