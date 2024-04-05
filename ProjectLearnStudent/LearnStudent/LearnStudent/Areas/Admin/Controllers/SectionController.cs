using LearnS.DataAccess.Data;
using LearnS.DataAccess.Repository.IRepository;
using LearnS.Models;
using LearnS.Models.ViewModels;
using LearnS.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace LearnStudent.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class SectionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public SectionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Section> objSectionList = _unitOfWork.Section.GetAll(includeProperties: "Category").ToList();

            return View(objSectionList);
        }

        public IActionResult Upsert(int? id)
        {
            SectionVM sectionVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Section = new Section()
            };
            if (id == null || id == 0)
            {
                //create
                return View(sectionVM);
            }
            else
            {
                //update
                sectionVM.Section = _unitOfWork.Section.Get(u => u.Id == id);
                return View(sectionVM);
            }

        }
        [HttpPost]
        public IActionResult Upsert(SectionVM sectionVM)
        {


            if (ModelState.IsValid)
            {
                if (sectionVM.Section.Id == 0)
                {
                    _unitOfWork.Section.Add(sectionVM.Section);
                }
                else
                {
                    _unitOfWork.Section.Update(sectionVM.Section);
                }
                _unitOfWork.Save();
                TempData["success"] = "Section created successfully";
                return RedirectToAction("Index");
            }
            else
            {

                {
                    sectionVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem

                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    });


                };
                return View(sectionVM);
            }
        }

        




        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Section> objSectionList = _unitOfWork.Section.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objSectionList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var sectionToBeDeleted = _unitOfWork.Section.Get(u=>u.Id == id);
            if(sectionToBeDeleted == null)
            {
            return Json(new { success = false, message = "Error while deleting" });

            }
            _unitOfWork.Section.Remove(sectionToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }
         

        #endregion
    }
}
