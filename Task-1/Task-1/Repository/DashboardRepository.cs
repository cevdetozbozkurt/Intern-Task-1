using Microsoft.EntityFrameworkCore;
using Task_1.Data;
using Task_1.Interfaces;
using Task_1.Models;

namespace Task_1.Repository
{
    public class DashboardRepository : IDashboardRepository
    {

        private readonly ApplicationDbContext _context;

        public DashboardRepository(ApplicationDbContext context)
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
			return await _context.Products.ToListAsync();
		}

		public async Task<Product> GetProductById(int id)
		{
			return await _context.Products.FindAsync(id);
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public bool UpdateProduct(Product product)
		{
			_context.Update(product);
			return Save();
		}
	}
}
