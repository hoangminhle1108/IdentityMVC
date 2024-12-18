using IdentityCus_Temp.Data;
using IdentityCus_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityCus_Temp.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Category cate { get; set; }
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            _db.Categories.Add(cate);
            _db.SaveChanges();
            TempData["success"] = "Category is created successfully";
            return RedirectToPage("Index");
        }
    }
}
