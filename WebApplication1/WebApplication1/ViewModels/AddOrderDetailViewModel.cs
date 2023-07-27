using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
	public class AddOrderDetailViewModel
	{
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
