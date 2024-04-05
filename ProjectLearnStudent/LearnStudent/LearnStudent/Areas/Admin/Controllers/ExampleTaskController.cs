using LearnS.DataAccess.Repository.IRepository;
using LearnS.Models;
using LearnS.Models.ViewModels;
using LearnS.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace LearnStudent.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ExampleTaskController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExampleTaskController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<ExampleTasks> exampleTasksList = _unitOfWork.ExampleTask.GetAll(includeProperties: "Section").ToList();
            return View(exampleTasksList);
        }

        public IActionResult Upsert(int? id)
        {
            ExampleTasksVM exampleTasksVM = new()
            {
                SectionList = _unitOfWork.Section.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Title,
                    Value = u.Id.ToString()
                }).ToList(),
                ExampleTasks = new ExampleTasks()
            };

            if (id == null || id == 0)
            {
                // Create
                return View(exampleTasksVM);
            }
            else
            {
                // Update
                exampleTasksVM.ExampleTasks = _unitOfWork.ExampleTask.Get(u => u.Id == id);
                return View(exampleTasksVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ExampleTasksVM exampleTasksVM)
        {
            if (ModelState.IsValid)
            {
                if (exampleTasksVM.ExampleTasks.Id == 0)
                {
                    _unitOfWork.ExampleTask.Add(exampleTasksVM.ExampleTasks);
                }
                else
                {
                    _unitOfWork.ExampleTask.Update(exampleTasksVM.ExampleTasks);
                }

                _unitOfWork.Save();
                TempData["success"] = "ExampleTask created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                exampleTasksVM.SectionList = _unitOfWork.Section.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Title,
                    Value = u.Id.ToString()
                }).ToList();

                return View(exampleTasksVM);
            }
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<ExampleTasks> exampleTasksList = _unitOfWork.ExampleTask.GetAll(includeProperties: "Section").ToList();
            return Json(new { data = exampleTasksList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var exampleTasksToBeDeleted = _unitOfWork.ExampleTask.Get(u => u.Id == id);
            if (exampleTasksToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.ExampleTask.Remove(exampleTasksToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}