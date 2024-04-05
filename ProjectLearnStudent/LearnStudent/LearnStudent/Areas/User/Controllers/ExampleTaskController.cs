using LearnS.DataAccess.Repository.IRepository;
using LearnS.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearnStudent.Areas.User.Controllers
{
    [Area("User")]
    public class ExampleTaskController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public ExampleTaskController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index(string section)
        {
            IEnumerable<ExampleTasks> exampletaskslist;

            if (!string.IsNullOrEmpty(section))
            {
                exampletaskslist = _unitOfWork.ExampleTask.GetAll(
                    filter: lm => lm.Section.Title == section,
                    includeProperties: "Section"
                    );
            }
            else
            {
                exampletaskslist = _unitOfWork.ExampleTask.GetAll(includeProperties: "Section");
            }
            ViewBag.Section = section;
        
        
            return View(exampletaskslist);
        }
    }
}
