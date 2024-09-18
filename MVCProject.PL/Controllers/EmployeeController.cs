using Microsoft.AspNetCore.Mvc;
using MVCProject.BLL.Interfaces;
using MVCProject.DAL.Entities;
using System;
using System.Linq;

namespace MVCProject.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository db;
        private readonly IDepartmentRepository _deptRepo;

        public EmployeeController(IEmployeeRepository Db,IDepartmentRepository DeptRepo)
        {
            db = Db;
            _deptRepo = DeptRepo;
        }
        public IActionResult Index()
        {

            //viewData it is a Dictionary obj int .net framework 3.5 
            // that used to transfer data from action to view
            //it is va KeyValuePair

            //ViewData["message"] = "Hello from View Data";

            //viewBag it is a  dynamic property int .net framework 4.0
            // that used to transfer data from action to view
            //it is  a property
            ViewBag.Message = "Hello from View Bag";

            var emp = db.GetAll();
            return View(emp);
        }

        public IActionResult Create()
        {
            ViewBag.depts = _deptRepo.GetAll();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee emp)
        {
            if (ModelState.IsValid)
            {
                var count=db.Add(emp);
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
            return View(res);


        }
        [HttpGet]
        public IActionResult Edit(int? id) {
            ViewBag.depts = _deptRepo.GetAll();

            return Details(id, "Edit");
        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee emp,[FromRoute]int id)
        {

            if (id != emp.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(emp);
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
        public IActionResult Delete(Employee emp, [FromRoute] int id)
        {

            if (id != emp.Id) return BadRequest();
            try { 
                
                    db.Delete(emp);
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
