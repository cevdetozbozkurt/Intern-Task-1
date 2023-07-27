using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
	public class OrderListViewModel
	{
		public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<Member> Members { get; set; }
    }
}
