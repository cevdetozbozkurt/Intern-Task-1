using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
	public interface IOrderDetailRepository
	{
		Task<IEnumerable<OrderDetail>> GetAllOrderDetails(); // Tüm sipariş detaylarını getiren metod
		IEnumerable<OrderDetail> GetOrderDetailsByMemberId(string memberId);
		Task<OrderDetail> GetOrderDetailById(int id); // Id'ye göre sipariş detayı getiren metod
		bool Add(OrderDetail orderDetail); // Yeni sipariş detayı ekleyen metod
		bool Update(OrderDetail orderDetail); // Sipariş detayı güncelleyen metod
		bool Delete(OrderDetail orderDetail); // Sipariş detayı silen metod
		bool Save();
	}
}
