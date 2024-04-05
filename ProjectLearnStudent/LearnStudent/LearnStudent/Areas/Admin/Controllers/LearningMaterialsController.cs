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
    public class LearningMaterialsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public LearningMaterialsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<LearningMaterials> objLearningMaterialsList = _unitOfWork.LearningMaterials.GetAll(includeProperties: "Section").ToList();

            return View(objLearningMaterialsList);
        }
        public IActionResult Upsert(int? id)
        {
            LearningMaterialsVM learningmaterialsVM = new()
            {
                SectionList = _unitOfWork.Section.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Title,
                    Value = u.Id.ToString()
                }),
                LearningMaterials = new LearningMaterials()
            };
            if (id == null || id == 0)
            {
                //create
                return View(learningmaterialsVM);
            }
            else
            {
                //update
                learningmaterialsVM.LearningMaterials = _unitOfWork.LearningMaterials.Get(u => u.Id == id);
                return View(learningmaterialsVM);
            }

        }

        [HttpPost]
        public IActionResult Upsert(LearningMaterialsVM learningMaterialsVM)
        {


            if (ModelState.IsValid)
            {
                if (learningMaterialsVM.LearningMaterials.Id == 0)
                {
                    _unitOfWork.LearningMaterials.Add(learningMaterialsVM.LearningMaterials);

                }
                else
                {
                    _unitOfWork.LearningMaterials.Update(learningMaterialsVM.LearningMaterials);
                }
                _unitOfWork.Save();
                TempData["success"] = "Materials created successfully";
                return RedirectToAction("Index");
            }
            else
            {

                {
                    learningMaterialsVM.SectionList = _unitOfWork.Section.GetAll().Select(u => new SelectListItem

                    {
                        Text = u.Title,
                        Value = u.Id.ToString()
                    });


                };
                return View(learningMaterialsVM);
            }
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<LearningMaterials> objLearningMaterialsList = _unitOfWork.LearningMaterials.GetAll(includeProperties: "Section").ToList();
            return Json(new { data = objLearningMaterialsList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var learningmaterialsToBeDeleted = _unitOfWork.LearningMaterials.Get(u => u.Id == id);
            if (learningmaterialsToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });

            }
            _unitOfWork.LearningMaterials.Remove(learningmaterialsToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }


#endregion
    }
}
