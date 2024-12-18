using IdentityCus.DataAccess.Data;
using IdentityCus.DataAccess.Repository.IRepository;
using IdentityCus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityCus.DataAccess.Repository
{
    public class ProductImageRepository : Repository<ProductImage>, IProductImageRepository
    {
        private ApplicationDbContext _db;
        public ProductImageRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }



        public void Update(ProductImage obj)
        {
            _db.ProductImages.Update(obj);
        }
    }
}
