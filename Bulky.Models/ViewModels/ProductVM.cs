using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace IdentityCus.Models.ViewModels
{
    public class ProductVM
    {
        public Product product { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> cateList {  get; set; } 
    }
}
