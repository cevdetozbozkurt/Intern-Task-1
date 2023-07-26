using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Data.Enum;

namespace WebApplication1.Models
{
	public class Order
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public DateTime Date { get; set; }

		[Required]
		[DataType(DataType.Currency)]
		public decimal Total { get; set; }

		[Required]
		public OrderStatus Status { get; set; }

		[Required]
		public string MemberId { get; set; }

		[ForeignKey("MemberId")]
		public virtual Member Member { get; set; }

		public virtual ICollection<OrderDetail> OrderDetails { get; set; }
	}
}
