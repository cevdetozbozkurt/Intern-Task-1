using Task_1.Models;

namespace Task_1.Interfaces
{
	public interface ICategoryRepository
	{
		Task<IEnumerable<Category>> GetAll();
		bool Add(Category category);
		bool Update(Category category);
		bool Delete(Category category);
		bool Save();

	}
}
