using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
	public class ProductListViewModel
	{
		public IEnumerable<Product> Products { get; set; }
		public IEnumerable<Category> Categories { get; set; }
	}
}
