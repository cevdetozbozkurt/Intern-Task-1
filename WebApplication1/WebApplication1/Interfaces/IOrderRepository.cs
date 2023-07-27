using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
	public interface IOrderRepository
	{
		Task<IEnumerable<Order>> GetAllOrders();
		IEnumerable<Order> GetOrdersByMemberId(string memberId);// Tüm siparişleri getiren metod
		Task<Order> GetOrderById(int id); // Id'ye göre sipariş getiren metod
		bool Add(Order order); // Yeni sipariş ekleyen metod
		bool Update(Order order); // Sipariş güncelleyen metod
		bool Delete(Order order); // Sipariş silen metod
		bool Save(); // Metotlari kayit eden metot
	}
}
