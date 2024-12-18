using IdentityCus.DataAccess.Data;
using IdentityCus.DataAccess.Repository.IRepository;
using IdentityCus.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityCus.DataAccess.Repository
{
    public class ProductRepository: Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Product pro)
        {
            var obj = _db.Products.FirstOrDefault(u =>  u.Id == pro.Id);
            if (obj != null)
            {
                obj.Title = pro.Title;
                obj.ISBN = pro.ISBN;
                obj.Price = pro.Price;
                obj.Price50 = pro.Price50;
                obj.ListPrice = pro.ListPrice;
                obj.Price100 = pro.Price100;
                obj.Description = pro.Description;
                obj.CategoryId = pro.CategoryId;
                obj.Author = pro.Author;
                obj.ProductImages = pro.ProductImages;  
            }
        }
    }
}

