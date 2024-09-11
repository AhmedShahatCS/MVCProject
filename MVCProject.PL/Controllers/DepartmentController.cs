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
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(dept);
        }

        public IActionResult Details(int id)
        {
            if (id == null) return BadRequest();
            var res = departRepo.Get(id);
            if (res == null) return NotFound();
            return View(res);

        }
    }
}
