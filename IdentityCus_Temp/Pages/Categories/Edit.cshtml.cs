using IdentityCus_Temp.Data;
using IdentityCus_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityCus_Temp.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        
        public Category cate { get; set; }
        public EditModel(ApplicationDbContext db)
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
            if (ModelState.IsValid)
            {
                _db.Categories.Update(cate);
                _db.SaveChanges();
                TempData["success"] = "Category is updated successfully";
                return RedirectToPage("Index", "Category");
            }
            return Page();
        }
    }
}
