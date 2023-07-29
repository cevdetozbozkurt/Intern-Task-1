using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
	public class OrderRepository : IOrderRepository
	{
		private readonly ApplicationDbContext _context;	

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Order order)
		{
			_context.Orders.Add(order);
			return Save();
		}

		public bool Delete(Order order)
		{
			_context.Remove(order);
			return Save();
		}

		public async Task<IEnumerable<Order>> GetAllOrders()
		{
			return await _context.Orders.ToListAsync();
		}

		public async Task<Order> GetOrderById(int id)
		{
			return await _context.Orders.Include(x=>x.OrderDetails).FirstOrDefaultAsync(y=>y.Id == id);
		}

		public bool Update(Order order)
		{
			_context.Update(order);
			return Save();
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public IEnumerable<Order> GetOrdersByMemberId(string memberId)
		{
			return  _context.Orders.Where(o => o.MemberId == memberId).Include(o => o.Member).Include(o => o.OrderDetails);
		}
	}
}
