using IdentityCus.Models;
using IdentityCus.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using IdentityCus.DataAccess.Repository;
using IdentityCus.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using IdentityCus.Models.ViewModels;
using IdentityCus.Utility;
using Microsoft.AspNetCore.Authorization;
using static System.Net.Mime.MediaTypeNames;

namespace IdentityCus.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork uow, IWebHostEnvironment webHostEnvironment)
        {
            _uow = uow;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> proList = _uow.Product.GetAll(includeProperties: "Category").ToList();

            return View(proList);
        }
        public IActionResult UpSert(int? id)
        {
            ProductVM product = new()
            {
                cateList = _uow.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }),
                product = new Product()
            };
            if (id == null || id == 0)
            {

                return View(product);
            }
            else
            {
                product.product = _uow.Product.Get(u => u.Id == id,includeProperties: "ProductImages");
                return View(product);
            }
        }
        //[HttpPost]
        //public IActionResult Create(Product pro)
        //{            
        //    if (ModelState.IsValid)
        //    {
        //        _uow.Product.Add(pro);
        //        _uow.Save();
        //        TempData["success"] = "Product is created successfully";
        //        return RedirectToAction("Index", "Product");
        //    }
        //    return View();
        //}
        [HttpPost]
        public IActionResult UpSert(ProductVM pro, List<IFormFile?> files)
        {
            if (ModelState.IsValid)
            {
                if (pro.product.Id == 0)
                {
                    _uow.Product.Add(pro.product);
                    TempData["success"] = "Product is created successfully";
                }
                else
                {
                    _uow.Product.Update(pro.product);
                    TempData["success"] = "Product is updated successfully";
                }

                _uow.Save();


                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (files != null)
                {

                    foreach (IFormFile file in files)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string productPath = @"images\products\product-" + pro.product.Id;
                        string finalPath = Path.Combine(wwwRootPath, productPath);
                        if (!Directory.Exists(finalPath))
                            Directory.CreateDirectory(finalPath);

                        using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        ProductImage productImage = new()
                        {
                            ImgUrl = @"\" + productPath + @"\" + fileName,
                            ProductId = pro.product.Id,
                        };

                        if (pro.product.ProductImages == null)
                        {
                            pro.product.ProductImages = new List<ProductImage>();
                            pro.product.ProductImages.Add(productImage);   
                            
                          
                        }

                        _uow.Product.Update(pro.product);
                        _uow.Save();
                    }

                    //if (!string.IsNullOrEmpty(pro.product.ImgUrl))
                    //{
                    //    var oldImgPath = Path.Combine(wwwRootPath, pro.product.ImgUrl.TrimStart('\\'));
                    //    if (System.IO.File.Exists(oldImgPath))
                    //    {
                    //        System.IO.File.Delete(oldImgPath);
                    //    }
                    //}
                    //using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    //{
                    //    file.CopyTo(fileStream);
                    //}
                    //pro.product.ImgUrl = @"\images\product\" + fileName;
                }


                return RedirectToAction("Index", "Product");
            }
            else
            {
                pro.cateList = _uow.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                });
                return View(pro);

            }
        }
        public IActionResult DeleteImage(int imageId)
        {
            var imageToBeDeleted = _uow.ProductImage.Get(u => u.Id == imageId);
            int productId = imageToBeDeleted.ProductId;
            if (imageToBeDeleted != null)
            {
                if (!string.IsNullOrEmpty(imageToBeDeleted.ImgUrl))
                {
                    var oldImagePath =
                                   Path.Combine(_webHostEnvironment.WebRootPath,
                                   imageToBeDeleted.ImgUrl.TrimStart('\\'));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                _uow.ProductImage.Remove(imageToBeDeleted);
                _uow.Save();

                TempData["success"] = "Deleted successfully";
            }

            return RedirectToAction(nameof(UpSert), new { id = productId });
        }
        //public IActionResult Edit(int? Id)
        //{
        //    if (Id == null && Id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Product? pro = _uow.Product.Get(u => u.Id == Id);
        //    if (pro == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(pro);
        //}
        //[HttpPost]
        //public IActionResult Edit(Product pro)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        _uow.Product.Update(pro);
        //        _uow.Save();
        //        TempData["success"] = "Product is updated successfully";
        //        return RedirectToAction("Index", "Product");
        //    }
        //    return View();

        //}

        //public IActionResult Delete(int? Id)
        //{
        //    if (Id == null && Id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Product? pro = _uow.Product.Get(u => u.Id == Id);
        //    if (pro == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(pro);
        //}
        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePOST(int? Id)
        //{
        //    Product pro = _uow.Product.Get(u => u.Id == Id);
        //    if (pro == null)
        //    {
        //        return NotFound();
        //    }
        //    _uow.Product.Remove(pro);
        //    _uow.Save();
        //    TempData["success"] = "Product is deleted successfully";
        //    return RedirectToAction("Index", "Product");
        //}
        #region API Calls
        [HttpGet]
        [Route("admin/product/getall")]
        public IActionResult GetAll()
        {
            List<Product> proList = _uow.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = proList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productToBeDeleted = _uow.Product.Get(u => u.Id == id);
            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            string productPath = @"images\products\product-" + id;
            string finalPath = Path.Combine(_webHostEnvironment.WebRootPath, productPath);

            if (Directory.Exists(finalPath))
            {
                string[] filePaths = Directory.GetFiles(finalPath);
                foreach (string filePath in filePaths)
                {
                    System.IO.File.Delete(filePath);
                }

                Directory.Delete(finalPath);
            }


            _uow.Product.Remove(productToBeDeleted);
            _uow.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}
