using LearnS.DataAccess.Repository.IRepository;
using LearnS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LearnStudent.Areas.User.Controllers
{
    [Area("User")]
    public class QuizController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        public QuizController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public IActionResult Index(string section)
        {
            IEnumerable<Quiz> quizList;

            if (!string.IsNullOrEmpty(section))
            {
                quizList = _unitOfWork.Quiz.GetAll(
                    filter: q => q.Section.Title == section,
                    includeProperties: "Section,Questions"
                );
            }
            else
            {
                quizList = _unitOfWork.Quiz.GetAll(includeProperties: "Section,Questions");
            }

            ViewBag.Section = section;

            return View(quizList);
        }







        public IActionResult CheckAnswer()
        {
            return View("CheckAnswer");
        }

        [HttpPost]
        public async Task<IActionResult> CheckAnswersAsync(Dictionary<string, string> answers, string section)
        {
            int correctAnswers = 0;
            int totalQuestions = answers.Count;

            foreach (var questionId in answers.Keys)
            {
                string selectedAnswer = answers[questionId];

                if (int.TryParse(questionId.Replace("question-", ""), out int parsedQuestionId))
                {
                    var question = _unitOfWork.Question.Get(q => q.Id == parsedQuestionId, includeProperties: "Quiz");

                    if (question != null)
                    {
                        bool isCorrect = (question.IsCorrect == 1 && selectedAnswer == "1") ||
                                         (question.IsCorrect == 2 && selectedAnswer == "2") ||
                                         (question.IsCorrect == 3 && selectedAnswer == "3") ||
                                         (question.IsCorrect == 4 && selectedAnswer == "4");

                        if (isCorrect)
                        {
                            correctAnswers++;
                        }
                    }
                }
            }

            
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                
                var quiz = _unitOfWork.Quiz.Get(q => q.Section.Title == section);

                if (quiz != null)
                {
                    int coinsForQuiz = quiz.Coins; 
                    user.Coins += coinsForQuiz;
                    int pointsForQuiz = quiz.Points;
                    user.Points += quiz.Points;

                    // Zapisz zmiany w bazie danych
                    await _userManager.UpdateAsync(user);
                }
            }

            ViewBag.Section = section;
            var result = Tuple.Create(correctAnswers, totalQuestions);
            return View("CheckAnswer", result);
        }
    }
}