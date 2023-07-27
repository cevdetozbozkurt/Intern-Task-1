using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
	public class OrderDetailLListViewModel
	{
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
