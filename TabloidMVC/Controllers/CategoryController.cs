using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TabloidMVC.Repositories;
using TabloidMVC.Models;
using System;

namespace TabloidMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;

       
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepo = categoryRepository;
        }
        
        public ActionResult Index()
        {
            List<Category> categories = _categoryRepo.GetAll();
            List<Category> sortedList = categories.OrderBy(x => x.Name).ToList();
            return View(sortedList);
        }
        
        public ActionResult Details(int id)
        {
            return View();
        }
        
       
        public ActionResult Delete(int id)
        {
            Category category = _categoryRepo.GetCategoryById(id);
            return View(category);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Category category)
        {
            try
            {
                _categoryRepo.DeleteCategory(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(category);
            }
        }
    }
}
    
   