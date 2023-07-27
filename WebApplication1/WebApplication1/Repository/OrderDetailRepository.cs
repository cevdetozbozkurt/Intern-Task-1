using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
	public class OrderDetailRepository : IOrderDetailRepository
	{

		private readonly ApplicationDbContext _context;

        public OrderDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(OrderDetail orderDetail)
		{
			_context.Add(orderDetail);
			return Save();
		}

		public bool Delete(OrderDetail orderDetail)
		{
			_context.Remove(orderDetail);
			return Save();
		}

		public async Task<IEnumerable<OrderDetail>> GetAllOrderDetails()
		{
			return await _context.OrderDetails.ToListAsync();
		}

		public async Task<OrderDetail> GetOrderDetailById(int id)
		{
			return await _context.OrderDetails.FindAsync(id);
		}

		public IEnumerable<OrderDetail> GetOrderDetailsByMemberId(string memberId)
		{
			return _context.OrderDetails.Where(od => od.Order.MemberId == memberId).Include(od => od.Order).Include(od => od.Product);
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public bool Update(OrderDetail orderDetail)
		{
			_context.Update(orderDetail);
			return Save();
		}
	}
}
