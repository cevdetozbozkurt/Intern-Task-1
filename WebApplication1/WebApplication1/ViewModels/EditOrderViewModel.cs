using System.ComponentModel.DataAnnotations;
using WebApplication1.Data.Enum;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
	public class EditOrderViewModel
	{
		public int Id { get; set; } // Sipariş id'si

		public DateTime Date { get; set; } // Sipariş tarihi

		[DataType(DataType.Currency)]
		public decimal Total { get; set; } // Sipariş toplamı

		public OrderStatus Status { get; set; } // Sipariş durumu
        public Member Member { get; set; }
        public virtual IEnumerable<OrderDetail> OrderDetails { get; set; }
	}
}
