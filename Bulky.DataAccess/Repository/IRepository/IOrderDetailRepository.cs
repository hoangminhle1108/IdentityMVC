using IdentityCus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityCus.DataAccess.Repository.IRepository
{
	public interface IOrderDetailRepository : IRepository<OrderDetails>
	{
		void Update(OrderDetails obj);
	}

}
