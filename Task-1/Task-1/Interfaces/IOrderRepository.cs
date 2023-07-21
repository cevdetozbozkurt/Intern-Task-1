using Task_1.Models;

namespace Task_1.Interfaces
{
	public interface IOrderRepository
	{

		Task<IEnumerable<Product>> GetAllProducts();
		bool Add(Product product);
		bool Delete(Product product);
		bool Save();
	}
}
