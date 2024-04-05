using LearnS.DataAccess.Repository.IRepository;
using LearnS.Models;
using LearnS.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnStudent.Areas.User.Controllers
{
    [Area("User")]
    public class LearningMaterialsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public LearningMaterialsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(string section)
        {
            IEnumerable<LearningMaterials> learningmaterialslist;

            if (!string.IsNullOrEmpty(section))
            {
                learningmaterialslist = _unitOfWork.LearningMaterials.GetAll(
                    filter: lm => lm.Section.Title == section,
                    includeProperties: "Section"
                );
            }
            else
            {
                learningmaterialslist = _unitOfWork.LearningMaterials.GetAll(includeProperties: "Section");
            }

            ViewBag.Section = section; 

            return View(learningmaterialslist);
        }
    }
}
