using Microsoft.AspNetCore.Mvc;
using MVCProject.BLL.Interfaces;
using MVCProject.DAL.Entities;

namespace MVCProject.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository departRepo;

        public DepartmentController(IDepartmentRepository  DepartRepo)
        {
            departRepo = DepartRepo;
        }
        public IActionResult Index()
        {
            var res = departRepo.GetAll();
            return View(res);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department dept)
        {
            if (ModelState.IsValid)
            {
                var count = departRepo.Add(dept);
                if (count > 0)
                {
                    //tempdata it is a dictionary obj KeyValuePair
                    // it is used to transfer data from action to action 
                    TempData["Message"] = "Dept is created";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(dept);
        }

        public IActionResult Details(int? id,string viewName="Details")
        {
            if (id == null) return BadRequest();
            var res = departRepo.Get(id.Value);
            if (res == null) return NotFound();
            return View(res);

        }

        public IActionResult Edit(int? id)
        {
            //if (id is null) return BadRequest();
            //var res = departRepo.Get(id.Value);
            //if (res == null) return NotFound();

            //return View(res);
            return Details(id,"Edit");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Department dept,[FromRoute] int id)
        {
            if(id !=dept.Id) return BadRequest();   

            if (ModelState.IsValid)
            {
                try
                {
                    departRepo.Update(dept);
                    return RedirectToAction(nameof(Index));

                }

                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);

                }
            }
            return View(dept);


        }

        [HttpGet]
        public IActionResult Delete(int? id) {

            //if (id is null) return BadRequest();
            //var res = departRepo.Get(id.Value);
            //if (res == null) return NotFound();
            //return View(res);
            return Details(id, "Delete");
            
        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Department dept, [FromRoute] int id)
        {
            if (id != dept.Id) return BadRequest();
            try
                {

                    departRepo.Delete(dept);
                    return RedirectToAction(nameof(Index));

                }

                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                return View(dept);
            }
          
            
           


        }


    }
}
