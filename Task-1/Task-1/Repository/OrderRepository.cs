using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Task_1.Data;
using Task_1.Interfaces;
using Task_1.Models;

namespace Task_1.Repository
{
	public class OrderRepository : IOrderRepository
	{
		private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Product product)
		{
			_context.Add(product);
			return Save();
		}

		public bool Delete(Product product)
		{
			_context.Remove(product);
			return Save();
		}

		public async Task<IEnumerable<Product>> GetAllProducts()
		{
			var orders = await _context.Orders.ToListAsync();
			var products = new List<Product>();
			foreach (var order in orders)
			{
				var product = await _context.Products.FindAsync(order.ProductId);
				products.Add(product);
			}
			return products;
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}
	}
}
