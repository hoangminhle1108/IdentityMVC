using IdentityCus_Temp.Data;
using IdentityCus_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityCus_Temp.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public List<Category> cateList { get; set; }
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
            cateList = _db.Categories.ToList();
        }
    }
}
