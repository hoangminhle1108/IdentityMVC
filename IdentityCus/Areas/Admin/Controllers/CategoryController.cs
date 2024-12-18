using IdentityCus.Models;
using IdentityCus.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using IdentityCus.DataAccess.Repository;
using IdentityCus.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using IdentityCus.Utility;

namespace IdentityCus.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _uow;
        public CategoryController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public IActionResult Index()
        {
            List<Category> cateList = _uow.Category.GetAll().ToList();
            return View(cateList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category cate)
        {
            if (cate.Name == cate.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Display Order need to be different from Name");
            }
            if (ModelState.IsValid)
            {
                _uow.Category.Add(cate);
                _uow.Save();
                TempData["success"] = "Category is created successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        public IActionResult Edit(int? Id)
        {
            if (Id == null && Id == 0)
            {
                return NotFound();
            }
            Category? cate = _uow.Category.Get(u => u.Id == Id);
            if (cate == null)
            {
                return NotFound();
            }
            return View(cate);
        }
        [HttpPost]
        public IActionResult Edit(Category cate)
        {

            if (ModelState.IsValid)
            {
                _uow.Category.Update(cate);
                _uow.Save();
                TempData["success"] = "Category is updated successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        public IActionResult Delete(int? Id)
        {
            if (Id == null && Id == 0)
            {
                return NotFound();
            }
            Category? cate = _uow.Category.Get(u => u.Id == Id);
            if (cate == null)
            {
                return NotFound();
            }
            return View(cate);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? Id)
        {
            Category cate = _uow.Category.Get(u => u.Id == Id);
            if (cate == null)
            {
                return NotFound();
            }
            _uow.Category.Remove(cate);
            _uow.Save();
            TempData["success"] = "Category is deleted successfully";
            return RedirectToAction("Index", "Category");
        }
    }
}
