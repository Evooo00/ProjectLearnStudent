using LearnS.DataAccess.Repository.IRepository;
using LearnS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LearnStudent.Areas.User.Controllers
{
    [Area("User")]
    public class AvatarShopController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public AvatarShopController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            IEnumerable<AvatarsUpload> avatarsList = _unitOfWork.AvatarsUpload.GetAll();
            return View(avatarsList);
        }

        public async Task<IActionResult> Purchase(int avatarId)
        {
            // Pobierz aktualnego użytkownika
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);

            // Sprawdź, czy użytkownik ma wystarczającą liczbę monet na zakup
            var avatar = await _unitOfWork.AvatarsUpload.FindAsync(avatarId);

            if (user.Coins < avatar.CoinsValue)
            {
                TempData["error"] = "Nie masz wystarczającej liczby monet na zakup tego avatara.";
                return RedirectToAction("Index");
            }

            // Dodaj rekord zakupu do bazy danych
            var purchase = new AvatarPurchase
            {
                UserId = userId,
                AvatarId = avatarId,
                PurchaseDate = DateTime.Now
            };
            _unitOfWork.AvatarPurchase.Add(purchase);
            _unitOfWork.Save();

            // Odejmij liczbę monet od użytkownika
            user.Coins -= avatar.CoinsValue;
            _unitOfWork.User.Update(user);
            _unitOfWork.Save();

            TempData["success"] = "Avatar zakupiony pomyślnie!";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> PurchasedAvatars()
        {
            try
            {
                var userId = _userManager.GetUserId(User);

                // Load the user including the PurchasedAvatars navigation property
                var user = await _userManager.Users
                    .Include(u => u.PurchasedAvatars)
                        .ThenInclude(ap => ap.Avatar)
                    .FirstOrDefaultAsync(u => u.Id == userId);

                if (user == null)
                {
                    return NotFound($"Unable to load user with ID '{userId}'.");
                }

                // Access the PurchasedAvatars directly from the user
                var purchasedAvatars = user.PurchasedAvatars;

                return View(purchasedAvatars);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                TempData["error"] = "An error occurred while fetching purchased avatars.";
                return RedirectToAction("Index"); // Redirect to a meaningful location
            }
        }


    }
}
