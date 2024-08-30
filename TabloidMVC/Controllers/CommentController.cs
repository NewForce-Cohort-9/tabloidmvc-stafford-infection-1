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

        // GET: CommentController/Create
        //public ActionResult Create()
        //{
        //    var viewModel = new CommentViewModel ();
        //    return View(viewModel);
        //}

        //// POST: CommentController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(CommentViewModel vm)
        //{
        //    try
        //    {

        //        //return RedirectToAction(nameof(Index));
        //        vm.Comment.CreateDateTime = DateAndTime.Now; // "Comment" variable name from CommentViewModel
        //        vm.Comment.UserProfileId = GetCurrentUserProfileId(); // Scroll down to bottom, GetCurrentUserProfileId() is declared below 
        //        var comment = new Comment
        //        {
        //            Content = vm.Comment.Content,
        //            Subject = vm.Comment.Subject,
        //            PostId = vm.PostId,
        //            CreateDateTime = DateTime.Now, // Correctly setting the date
        //            UserProfileId = GetCurrentUserProfileId()
        //        };

        //        _commentRepository.Add(vm.Comment);

        //        return RedirectToAction("Index", new { id = vm.Comment.Id });
        //    }
        //    catch
        //    {
        //        return View(vm);
        //    }
        //}





        //// GET: CommentController/Create
        //public ActionResult Create()
        //{

        //    return View();
        //}
        ////post
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(Comment Comment)
        //{
        //    try
        //    {

        //        //return RedirectToAction(nameof(Index));
        //        Comment.CreateDateTime = DateAndTime.Now; // "Comment" variable name from CommentViewModel
        //        Comment.UserProfileId = GetCurrentUserProfileId(); // Scroll down to bottom, GetCurrentUserProfileId() is declared below 

        //        //var comment = new Comment
        //        //{
        //        //    Content = Comment.Content,
        //        //    Subject = Comment.Subject,
        //        //    PostId = Comment.PostId,
        //        //    CreateDateTime = DateTime.Now, // Correctly setting the date
        //        //    UserProfileId = GetCurrentUserProfileId()
        //        //};

        //        _commentRepository.Add(Comment);

        //        return RedirectToAction("Index", new { id = Comment.Id });
        //    }
        //    catch
        //    {
        //        return View(Comment);
        //    }
        //}




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


        //// POST: Comment/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(Comment comment)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            // Set properties for the new comment
        //            comment.CreateDateTime = DateTime.Now; // Set current date and time
        //            comment.UserProfileId = GetCurrentUserProfileId(); // Get current user profile ID

        //            // Add the new comment to the repository
        //            _commentRepository.Add(comment);

        //            // Redirect to the Index action or any other relevant action
        //            return RedirectToAction("Index", new { postId = comment.PostId });
        //        }
        //        catch (Exception ex)
        //        {
        //            // Log the exception or handle it appropriately
        //            // For debugging purposes, you can add logging here

        //            // Add a general error message
        //            ModelState.AddModelError("", "An error occurred while creating the comment. Please try again.");

        //            // Return the view with the current model to show validation errors
        //            return View(comment);
        //        }
        //    }

        //    // If model state is not valid, return the view with validation errors
        //    return View(comment);
        //}



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
