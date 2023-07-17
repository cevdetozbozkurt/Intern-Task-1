using Microsoft.EntityFrameworkCore;
using Task_1.Data;
using Task_1.Interfaces;
using Task_1.Models;

namespace Task_1.Repository
{
	public class ProductRepository : IProductRepository
	{

		private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
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

		public async Task<IEnumerable<Product>> GetAll()
		{
			return await _context.Products.ToListAsync();
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public bool Update(Product product)
		{
			_context.Update(product);
			return Save();
		}
	}
}
