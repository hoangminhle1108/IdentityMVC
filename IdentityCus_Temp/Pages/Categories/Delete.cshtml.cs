using IdentityCus_Temp.Data;
using IdentityCus_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityCus_Temp.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public Category cate { get; set; }
        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(int? id)
        {
            if (id != null && id != 0)
            {
                cate = _db.Categories.Find(id);
            }
        }
        public IActionResult OnPost()
        {
            Category category = _db.Categories.Find(cate.Id);
            if (category == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(category);
            _db.SaveChanges();
            TempData["success"] = "Category is deleted successfully";
            return RedirectToPage("Index");
        }
    }
}
