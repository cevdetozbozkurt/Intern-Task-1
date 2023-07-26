using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
	public interface IProductRepository
	{
		Task<IEnumerable<Product>> GetAll();
		Task<Product> GetById(int id);
		bool Add(Product product);
		bool Update(Product product);
		bool Delete(Product product);
		bool Save();
	}
}
