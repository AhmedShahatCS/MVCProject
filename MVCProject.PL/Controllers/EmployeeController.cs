using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MVCProject.BLL.Interfaces;
using MVCProject.DAL.Entities;
using MVCProject.PL.Helpers;
using MVCProject.PL.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace MVCProject.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
    //    private readonly IEmployeeRepository db;
    //    private readonly IDepartmentRepository _deptRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index( string valueSearch)
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
                emp =await _unitOfWork.EmpRepo.GetAllAsync();
            }
            else
            {
                emp = _unitOfWork.EmpRepo.GetByName(valueSearch);
            }

             
            var MappedEmp = _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeViewModel>>(emp);
            return View(MappedEmp);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.depts =await _unitOfWork.DeptRepo.GetAllAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel emp)
        {
            if (ModelState.IsValid)
            {
                emp.ImageName = DocumentSetting.UploadFile(emp.Image, "Images");
                var MappedEmp = _mapper.Map<EmployeeViewModel, Employee>(emp);
                await _unitOfWork.EmpRepo.AddAsync(MappedEmp);
                var count =await _unitOfWork.CompleteAsync();
                if (count > 0)
                    TempData["Message"] = "Employee Is Created";
                {
                    return RedirectToAction(nameof(Index));
                }
                
            }
            return View(emp);
        }

        public async Task<IActionResult> Details(int? id,string view="Details")
        {
            if (id is null) return BadRequest();
            var res =await _unitOfWork.EmpRepo.GetAsync(id.Value);
            if (res is null) return NotFound();
            var MappedEmp = _mapper.Map<Employee, EmployeeViewModel>(res);
            return View(MappedEmp);


        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id) {

			//ViewBag.depts = await _unitOfWork.DeptRepo.GetAllAsync();


			ViewBag.depts = await _unitOfWork.DeptRepo.GetAllAsync();

            return await Details(id, "Edit");
        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeViewModel emp,[FromRoute]int id)
        {

            if (id != emp.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    if(emp.Image is not null)
                    {
                      emp.ImageName=  DocumentSetting.UploadFile(emp.Image, "Images");
                    }
                    var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(emp);

                    _unitOfWork.EmpRepo.Update(mappedEmp);
                   await _unitOfWork.CompleteAsync();
                    return RedirectToAction(nameof(Index));
                }catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(emp);

        }

        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(EmployeeViewModel emp, [FromRoute] int id)
        {

            if (id != emp.Id) return BadRequest();
            try { 
                    var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(emp);

               _unitOfWork.EmpRepo.Delete(mappedEmp);
              var count=await  _unitOfWork.CompleteAsync();
                if(count>0&&emp.ImageName is not null)
                {
                    DocumentSetting.DeleteFile(emp.ImageName, "Images");
                }
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
