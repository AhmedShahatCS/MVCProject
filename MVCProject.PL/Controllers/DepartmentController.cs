using Microsoft.AspNetCore.Mvc;
using MVCProject.BLL.Interfaces;
using MVCProject.DAL.Entities;
using System.Threading.Tasks;

namespace MVCProject.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitofwork;

        //private readonly IDepartmentRepository departRepo;

        public DepartmentController(/*IDepartmentRepository  DepartRepo*/ IUnitOfWork unitofwork)
        {
            //departRepo = DepartRepo;
            _unitofwork = unitofwork;
        }
        public async Task<IActionResult> Index()
        {
            var res =await _unitofwork.DeptRepo.GetAllAsync();
            return View(res);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Department dept)
        {
            if (ModelState.IsValid)
            {
              await _unitofwork.DeptRepo.AddAsync(dept);
               
                var count =await _unitofwork.CompleteAsync();
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

        public async Task<IActionResult> Details(int? id,string viewName="Details")
        {
            if (id == null) return BadRequest();
            var res =await _unitofwork.DeptRepo.GetAsync(id.Value);
            if (res == null) return NotFound();
            return View(res);

        }

        public async Task<IActionResult> Edit(int? id)
        {
            //if (id is null) return BadRequest();
            //var res = departRepo.Get(id.Value);
            //if (res == null) return NotFound();

            //return View(res);
            return await Details(id,"Edit");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Department dept,[FromRoute] int id)
        {
            if(id !=dept.Id) return BadRequest();   

            if (ModelState.IsValid)
            {
                try
                {
                    _unitofwork.DeptRepo.Update(dept);
                    await _unitofwork.CompleteAsync();
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
        public async Task<IActionResult> Delete(int? id) {

            //if (id is null) return BadRequest();
            //var res = departRepo.Get(id.Value);
            //if (res == null) return NotFound();
            //return View(res);
            return await Details(id, "Delete");
            
        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Department dept, [FromRoute] int id)
        {
            if (id != dept.Id) return BadRequest();
            try
                {

                    _unitofwork.DeptRepo.Delete(dept);
               await _unitofwork.CompleteAsync();
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
