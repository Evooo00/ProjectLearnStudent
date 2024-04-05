using LearnS.DataAccess.Data;
using LearnS.DataAccess.Repository.IRepository;
using LearnS.Models;
using LearnS.Models.ViewModels;
using LearnS.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace LearnStudent.Areas.User.Controllers
{
    [Area("User")]

    public class ForumController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public ForumController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }


        public async Task<IActionResult> AskQuestion(int? id)
        {
            ForumVM forumVM = new ForumVM
            {
                ForumThread = new ForumThread
                {
                    CreatedAt = DateTime.Now,
                    User = await _userManager.GetUserAsync(User)
                }
            };

            if (id != null && id != 0)
            {
                forumVM.ForumThread = _unitOfWork.ForumThread.Get(u => u.Id == id);
            }

            return View(forumVM);
        }

        public async Task<IActionResult> ViewThreadAsync(int id)
        {
            var forumThread = _unitOfWork.ForumThread.Get(u => u.Id == id, includeProperties: "User,ForumPosts.ForumComments");

            if (forumThread == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            if (userId != null && (forumThread.ForumPosts?.All(fp => fp.UserId != userId) ?? true))
            {
                forumThread.NumberOfViews++;
                _unitOfWork.ForumThread.Update(forumThread);
                _unitOfWork.Save();
            }

            return View(forumThread);
        }



        private void UpdateReplyCount(int threadId)
        {
            var thread = _unitOfWork.ForumThread.Get(t => t.Id == threadId, includeProperties: "ForumPosts");

            if (thread != null)
            {
                thread.ReplyCount = thread.ForumPosts.Count;
                _unitOfWork.ForumThread.Update(thread);
                _unitOfWork.Save();
            }
        }






        public async Task<IActionResult> Index()
        {
            var forumThreads = await _unitOfWork.ForumThread.GetAllThreadsAsync(includeUser: true);

            foreach (var forumThread in forumThreads)
            {
                if (forumThread.ForumPosts != null && forumThread.ForumPosts.Any())
                {
                    forumThread.ForumPosts.First().NumberOfViews++;
                }
            }

            _unitOfWork.Save();

            return View(forumThreads);
        }

        [HttpPost]
        public async Task<IActionResult> AskQuestion(ForumVM forumVM)
        {
            var user = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {
                if (forumVM.ForumThread.Id == 0)
                {
                    forumVM.ForumThread.UserId = user.Id;
                    _unitOfWork.ForumThread.Add(forumVM.ForumThread);
                }
                else
                {
                    var existingThread = _unitOfWork.ForumThread.Get(u => u.Id == forumVM.ForumThread.Id);

                    if (existingThread == null)
                    {
                        return NotFound();
                    }

                    existingThread.Title = forumVM.ForumThread.Title;
                    existingThread.Content = forumVM.ForumThread.Content;
                    _unitOfWork.ForumThread.Update(existingThread);
                }

                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(forumVM);
        }




        [HttpPost]
        public async Task<IActionResult> AddReply(int id, string replyContent)
        {
            var forumThread = _unitOfWork.ForumThread.Get(u => u.Id == id, includeProperties: "ForumPosts.User,ForumPosts.ForumRatings,ForumPosts.ForumComments.User");

            if (forumThread == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            var forumPost = forumThread.ForumPosts.FirstOrDefault(fp => fp.UserId == user.Id);

            if (forumPost == null)
            {
                forumPost = new ForumPost
                {
                    Content = replyContent,
                    CreatedAt = DateTime.Now,
                    ForumThreadId = forumThread.Id,
                    User = user
                };

                _unitOfWork.ForumPost.Add(forumPost);
                _unitOfWork.Save();
            }
            else
            {
                var newReply = new ForumPost
                {
                    Content = replyContent,
                    CreatedAt = DateTime.Now,
                    ForumThreadId = forumThread.Id,
                    User = user
                };

                _unitOfWork.ForumPost.Add(newReply);
                _unitOfWork.Save();
            }

            UpdateReplyCount(forumThread.Id);

            return RedirectToAction("ViewThread", new { id = forumThread.Id });
        }


        [HttpPost]
        public async Task<IActionResult> AddComment(int replyId, string commentContent)
        {
            var forumPost = _unitOfWork.ForumPost.Get(u => u.Id == replyId, includeProperties: "ForumComments.User");

            if (forumPost == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);

            var newComment = new ForumComment
            {
                Content = commentContent,
                CreatedAt = DateTime.Now,
                ForumPostId = forumPost.Id,
                UserId = user.Id
            };

            _unitOfWork.ForumComment.Add(newComment);
            _unitOfWork.Save();

            return RedirectToAction("ViewThread", new { id = forumPost.ForumThreadId });
        }
        [HttpPost]

        public async Task<IActionResult> DeletePost(int postId)
        {
            var postToDelete = _unitOfWork.ForumPost.Get(p => p.Id == postId);

            if (postToDelete != null)
            {
                var commentsToDelete = _unitOfWork.ForumComment.Find(c => c.ForumPostId == postId);
                _unitOfWork.ForumComment.RemoveRange(commentsToDelete);

                _unitOfWork.ForumPost.Remove(postToDelete);
                _unitOfWork.Save();
            }

            return RedirectToAction("ViewThread", new { id = postToDelete?.ForumThreadId });
        }

        [HttpPost]
        public async Task<IActionResult> DeletePostByUser(int postId)
        {
            var user = await _userManager.GetUserAsync(User);
            var postToDelete = _unitOfWork.ForumPost.Get(p => p.Id == postId && p.UserId == user.Id);

            if (postToDelete != null)
            {
                var commentsToDelete = _unitOfWork.ForumComment.Find(c => c.ForumPostId == postId);
                _unitOfWork.ForumComment.RemoveRange(commentsToDelete);

                _unitOfWork.ForumPost.Remove(postToDelete);
                _unitOfWork.Save();
            }

            return RedirectToAction("ViewThread", new { id = postToDelete?.ForumThreadId });
        }
        [HttpPost]
        public async Task<IActionResult> AddRating(int postId, int rating)
        {
            var user = await _userManager.GetUserAsync(User);

          
            var existingRating = _unitOfWork.ForumRating.Get(r => r.ForumPostId == postId && r.UserId == user.Id);

            if (existingRating != null)
            {
               
                existingRating.Value = rating;
                _unitOfWork.ForumRating.Update(existingRating);
            }
            else
            {
               
                var newRating = new ForumRating
                {
                    Value = rating,
                    UserId = user.Id,
                    ForumPostId = postId
                };

                _unitOfWork.ForumRating.Add(newRating);
            }

            _unitOfWork.Save();

            
            var forumPost = _unitOfWork.ForumPost.Get(p => p.Id == postId);
            return RedirectToAction("ViewThread", new { id = forumPost.ForumThreadId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var commentToDelete = _unitOfWork.ForumComment.Get(u => u.Id == commentId, includeProperties: "ForumPost.ForumThread");

            if (commentToDelete == null)
            {
                return NotFound();
            }


            var userId = _userManager.GetUserId(User);
            if (commentToDelete.UserId != userId)
            {
                return Forbid();
            }

            _unitOfWork.ForumComment.Remove(commentToDelete);
            _unitOfWork.Save();

            return RedirectToAction("ViewThread", new { id = commentToDelete.ForumPost.ForumThreadId });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteThread(int threadId)
        {
            var threadToDelete = _unitOfWork.ForumThread.Get(t => t.Id == threadId);

            if (threadToDelete == null)
            {
                return NotFound();
            }

            var relatedPosts = _unitOfWork.ForumPost.Find(fp => fp.ForumThreadId == threadId);
            var relatedPostIds = relatedPosts.Select(fp => fp.Id);

            var relatedRatings = _unitOfWork.ForumRating.Find(r => relatedPostIds.Contains(r.ForumPostId));

            _unitOfWork.ForumRating.RemoveRange(relatedRatings);  
            _unitOfWork.ForumPost.RemoveRange(relatedPosts);
            _unitOfWork.ForumThread.Remove(threadToDelete);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }





        [HttpPost]
        public async Task<IActionResult> DeleteReply(int replyId)
        {
            var replyToDelete = _unitOfWork.ForumPost.Get(u => u.Id == replyId, includeProperties: "ForumComments,ForumRatings");

            if (replyToDelete == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            if (replyToDelete.UserId != userId)
            {
                return Forbid();
            }

            _unitOfWork.ForumRating.RemoveRange(replyToDelete.ForumRatings);
            _unitOfWork.ForumComment.RemoveRange(replyToDelete.ForumComments);

            _unitOfWork.ForumPost.Remove(replyToDelete);
            _unitOfWork.Save();

            UpdateReplyCount(replyToDelete.ForumThreadId);

            return RedirectToAction("ViewThread", new { id = replyToDelete.ForumThreadId });
        }






    }
}