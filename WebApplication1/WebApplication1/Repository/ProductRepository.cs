using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Repository
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

		public async Task<Product> GetById(int id)
		{
			return _context.Products.FirstOrDefault(x => x.Id == id);
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
