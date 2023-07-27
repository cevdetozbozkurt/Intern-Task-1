using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
	public class EditOrderDetailViewModel
	{
		public int Id { get; set; } // Sipariş detayı id'si

		public int OrderId { get; set; } // Sipariş id'si

		public int ProductId { get; set; } // Ürün id'si

		public int Quantity { get; set; } // Sipariş edilen miktar

		public IEnumerable<Order> Orders { get; set; } // Tüm siparişleri tutan liste
		public IEnumerable<Product> Products { get; set; } // Tüm ürünleri tutan liste
	}
}
