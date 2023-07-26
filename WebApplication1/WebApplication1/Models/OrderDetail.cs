using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
	public class OrderDetail
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int OrderId { get; set; }

		[ForeignKey("OrderId")]
		public Order Order { get; set; }

		[Required]
		public int ProductId { get; set; }

		[ForeignKey("ProductId")]
		public virtual Product Product { get; set; }

		[Required]
		public int Quantity { get; set; }
	}
}
