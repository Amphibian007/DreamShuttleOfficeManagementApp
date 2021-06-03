using AutoMapper;
using LeaveManagement.Contratcs;
using LeaveManagement.Data;
using LeaveManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveManagement.Controllers
{
    public class LeaveTypesController : Controller
    {
        private readonly ILeaveTypeRepository _repo;
        public IMapper _mapper;

        public LeaveTypesController(ILeaveTypeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: LeaveTypesController
        public ActionResult Index()
        {
            var leaveTypes = _repo.FindAll().ToList();
            var model=_mapper.Map<List<LeaveType>,List<LeaveTypeVM>>(leaveTypes);
            return View(model);
        }

        // GET: LeaveTypesController/Details/5
        public ActionResult Details(int id)
        {
            if (!_repo.IsExists(id))
            {
                return NotFound();
            }
            var leaveType = _repo.FindById(id);
            var model = _mapper.Map<LeaveTypeVM>(leaveType);
            return View(model);
        }

        // GET: LeaveTypesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LeaveTypeVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var leaveType = _mapper.Map<LeaveType>(model);
                leaveType.DateCreated = DateTime.Now;
                var isSuccess = _repo.Create(leaveType);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something Went Wrong...");
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something Went Wrong...");
                return View(model);
            }
        }

        // GET: LeaveTypesController/Edit/5
        public ActionResult Edit(int id)
        {
            if (!_repo.IsExists(id))
            {
                return NotFound();
            }
            var leaveType = _repo.FindById(id);
            var model = _mapper.Map<LeaveTypeVM>(leaveType);
            return View(model);
        }

        // POST: LeaveTypesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LeaveTypeVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var leaveType = _mapper.Map<LeaveType>(model);
                var isSuccess = _repo.Update(leaveType);
                if (!isSuccess)
                {
                    ModelState.AddModelError("","Something went wrong...");
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong...");
                return View();
            }
        }

        // GET: LeaveTypesController/Delete/5
        public ActionResult Delete(int id)
        {
            var leaveType = _repo.FindById(id);
            if (leaveType == null)
            {
                return NotFound();
            }
            var isSuccess = _repo.Delete(leaveType);
            if (!isSuccess)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
