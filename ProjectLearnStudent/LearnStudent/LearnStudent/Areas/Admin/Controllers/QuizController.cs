using LearnS.DataAccess.Repository.IRepository;
using LearnS.Models;
using LearnS.Models.ViewModels;
using LearnS.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace LearnStudent.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class QuizController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
       

        public QuizController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
           
        }

        public IActionResult Index()
        {
            List<Quiz> objQuizlsList = _unitOfWork.Quiz.GetAll(includeProperties: "Section").ToList();

            return View(objQuizlsList);
        }

        public IActionResult Upsert(int? id)
        {
            QuizVM quizVM = new()
            {
                SectionList = _unitOfWork.Section.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Title,
                    Value = u.Id.ToString()
                }),
                Quiz = new Quiz(),
                Questions = new List<Question>(),
               
            };

            if (id == null || id == 0)
            {
                return View(quizVM);
            }
            else
            {
                // Edit quiz
                quizVM.Quiz = _unitOfWork.Quiz.Get(u => u.Id == id);
                quizVM.Quiz.Points = quizVM.Quiz.Points;
                quizVM.Quiz.Coins = quizVM.Quiz.Coins;
                // Get question from quiz
                quizVM.Questions = _unitOfWork.Question.GetAll().Where(q => q.QuizId == id).ToList();
                return View(quizVM);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(QuizVM quizVM)
        {
            if (ModelState.IsValid)
            {
                if (quizVM.Quiz.Id == 0)
                {
                    // add new quiz
                    quizVM.Quiz.Points = quizVM.Quiz.Points;
                    quizVM.Quiz.Coins = quizVM.Quiz.Coins;
                    _unitOfWork.Quiz.Add(quizVM.Quiz);
                    _unitOfWork.Save(); // save changes to generate the id for the quiz

                    // add question
                    foreach (var question in quizVM .Questions)
                    {
                        question.QuizId = quizVM.Quiz.Id;
                        _unitOfWork.Question.Add(question);
                    }

                    _unitOfWork.Save();
                    TempData["success"] = "Quiz created successfully";
                }
                else
                {
                    // Update quiz
                    _unitOfWork.Quiz.Update(quizVM.Quiz);
                    quizVM.Quiz.Points = quizVM.Quiz.Points;
                    quizVM.Quiz.Coins = quizVM.Quiz.Coins;

                    // Update question
                    var existingQuestions = _unitOfWork.Question.GetAll().Where(q => q.QuizId == quizVM.Quiz.Id).ToList();

                    foreach (var question in existingQuestions)
                    {
                        _unitOfWork.Question.Remove(question);
                    }

                    foreach (var question in quizVM.Questions)
                    {
                        question.QuizId = quizVM.Quiz.Id;
                        _unitOfWork.Question.Add(question);
                    }
                }

                _unitOfWork.Save();

                TempData["success"] = "Quiz updated successfully";

                return RedirectToAction("Index");
            }
            else
            {
                // Back list
                quizVM.SectionList = _unitOfWork.Section.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Title,
                    Value = u.Id.ToString()
                });

                return View(quizVM);
            }
        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Quiz> objQuizList = _unitOfWork.Quiz.GetAll(includeProperties: "Section").ToList();
            return Json(new { data = objQuizList });

        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var QuizToBeDeleted = _unitOfWork.Quiz.Get(u => u.Id == id);
            if (QuizToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Quiz.Remove(QuizToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}

