using LearnS.DataAccess.Data;
using LearnS.DataAccess.Repository;
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
    public class AvatarsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
      
        public AvatarsController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment  )
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
           
        }

        public IActionResult Index()
        {
            List<AvatarsUpload> objAvatarsUploadList = _unitOfWork.AvatarsUpload.GetAll().ToList();
            return View(objAvatarsUploadList);
        }
        public IActionResult Upsert(int? id)
        {
            AvatarsUploadVM avatarsUploadVM = new()
            {
                AvatarsList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                AvatarsUpload = new AvatarsUpload()
            };
            if (id == null || id == 0)
            {
                //create
                return View(avatarsUploadVM);
            }
            else
            {
                //update
                avatarsUploadVM.AvatarsUpload = _unitOfWork.AvatarsUpload.Get(u => u.Id == id);
                return View(avatarsUploadVM);
            }

        }


        [HttpPost]
        public IActionResult Upsert(AvatarsUploadVM avatarsUploadVM, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string avatarpath = Path.Combine(wwwRootPath, @"images\Avatars");

                    if (!string.IsNullOrEmpty(avatarsUploadVM.AvatarsUpload.ImageUrl))
                    {
                        //delete the old image
                        var oldImagePath =
                            Path.Combine(wwwRootPath, avatarsUploadVM.AvatarsUpload.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(avatarpath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    avatarsUploadVM.AvatarsUpload.ImageUrl = @"\images\Avatars\" + fileName;
                }

                if (avatarsUploadVM.AvatarsUpload.Id == 0)
                {
                    _unitOfWork.AvatarsUpload.Add(avatarsUploadVM.AvatarsUpload);
                }
                else
                {
                    _unitOfWork.AvatarsUpload.Update(avatarsUploadVM.AvatarsUpload);
                }

                _unitOfWork.Save();
                TempData["success"] = "Avatar created successfully";
                return RedirectToAction("Index");
            }
            return View(avatarsUploadVM);
        }


        

    #region API CALLS
    [HttpGet]
    public IActionResult GetAll()
    {
        List<AvatarsUpload> objAvatarList = _unitOfWork.AvatarsUpload.GetAll().ToList();
        return Json(new { data = objAvatarList });
    }

    [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var avatarsToBeDeleted = _unitOfWork.AvatarsUpload.Get(u => u.Id == id);
            if (avatarsToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath =
                           Path.Combine(_webHostEnvironment.WebRootPath,
                           avatarsToBeDeleted.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.AvatarsUpload.Remove(avatarsToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}
