using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
	public class CartViewModel
	{
		public List<OrderDetail> Items { get; set; }

		public decimal TotalPrice
		{
			get
			{
				return Items.Sum(x => x.Product.Price * x.Quantity);
			}
		}
	}
}
