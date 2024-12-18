using IdentityCus.DataAccess.Repository.IRepository;
using IdentityCus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityCus.DataAccess.Repository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        void Update(ShoppingCart obj);
    }
}
