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
        private readonly IUserProfileRepository _userProfileRepository;


        public CommentController(ICommentRepository commentRepository, IPostRepository postRepository, IUserProfileRepository userProfileRepository)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _userProfileRepository = userProfileRepository;
        }

        // GET: CommentController
        public ActionResult Index(int postId)
        {

            var comments = _commentRepository.GetCommentsByPostId(postId);
            var post = _postRepository.GetPublishedPostById(postId); // Fetch post details

            // Get the current user's ID from authentication 
            var currentUserIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Convert the current user's ID to an integer
            int currentUserId = int.Parse(currentUserIdString); 

            var viewModel = new CommentViewModel
            {
                PostId = postId,
                PostTitle = post.Title, 
                Comments = comments,
                CurrentUserId = currentUserId // Pass the current user’s ID to the view model
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
                PostId = postId, 
                UserProfileId = GetCurrentUserProfileId() 
            };

            return View(comment);
        }

        // POST: Comment/Create
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


        // GET: Comment/Edit/5
        public IActionResult Edit(int id)
        {
            var comment = _commentRepository.GetCommentById(id);            
            
            if (comment == null)
            {
                return NotFound();
            }
            
                return View(comment);
          
        }

        // POST: Comment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Comment comment)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _commentRepository.Edit(id, comment);

                    return RedirectToAction("Index", new { postId = comment.PostId }); 
                    // Note: going back to Index, so look up at Index method in controller, it takes postId parameter so MUST HAVE ,new { postId = postId }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while updating the comment. Please try again.");
            
                }
            }

            return View(comment);
        }

        // GET: CommentController/Delete/5
        public IActionResult Delete(int id)
        {
            var comment = _commentRepository.GetCommentById(id);
            if (comment == null)
            {
                return NotFound();
            }
            return View(comment);
        }

        // POST: CommentController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var comment = _commentRepository.GetCommentById(id);
            if (comment == null)
            {
                return NotFound();
            }
            _commentRepository.Delete(id);
            return RedirectToAction("Index", new { postId = comment.PostId });
        }




        private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
