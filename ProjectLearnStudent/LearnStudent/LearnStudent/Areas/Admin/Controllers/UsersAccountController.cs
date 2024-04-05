using LearnS.DataAccess.Repository.IRepository;
using LearnS.Models;
using LearnS.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LearnStudent.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class UsersAccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersAccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var users = _unitOfWork.User.GetAll().ToList();

            return View(users);
        }
    }
}
