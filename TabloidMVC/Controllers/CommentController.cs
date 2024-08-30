using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Security.Claims;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;

        public CommentController(ICommentRepository commentRepository, IPostRepository postRepository)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
        }
        // GET: CommentController
        public ActionResult Index(int postId)
        {

            var comments = _commentRepository.GetCommentsByPostId(postId);
            var post = _postRepository.GetPublishedPostById(postId); // Adjusted method to fetch post details

            var viewModel = new CommentViewModel
            {
                PostId = postId,
                PostTitle = post?.Title ?? "Unknown Title", // Provide a default value if post is null
                Comments = comments
            };

            return View(viewModel);
        }

        // GET: CommentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Comment/Create
        public ActionResult Create(int postId)
        {
            var comment = new Comment
            {
                PostId = postId, //diff: Ensure PostId is passed
                UserProfileId = GetCurrentUserProfileId() // This should be set in the POST method
            };

            return View(comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Comment comment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    comment.CreateDateTime = DateTime.Now;
                    comment.UserProfileId = GetCurrentUserProfileId();

                    _commentRepository.Add(comment);

                    return RedirectToAction("Index", new { postId = comment.PostId });
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError("", "An error occurred while creating the comment. Please try again.");
                    Console.WriteLine("Error creating comment: " + ex.Message);
                    
                }
            }

            return View(comment);

        }


        // GET: CommentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CommentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CommentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CommentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
