using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
	public class AddOrderDetailViewModel
	{
        public int OrderId { get; set; }
        public IEnumerable<int> Quantity { get; set; }
        //public IEnumerable<Order> Orders { get; set; }
        public Order Order { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public Member Member { get; set; }
    }
}
