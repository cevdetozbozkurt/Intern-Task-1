using Task_1.Models;

namespace Task_1.Interfaces
{
	public interface IProductRepository
	{

		Task<IEnumerable<Product>> GetAll();
		bool Add(Product product);
		bool Update(Product product);
		bool Delete(Product product);
		bool Save();

	}
}
