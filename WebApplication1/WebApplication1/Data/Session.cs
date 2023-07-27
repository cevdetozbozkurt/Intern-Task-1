using WebApplication1.Models;

namespace WebApplication1.Data
{
	public static class Session
	{
		public static List<OrderDetail> session;
		static Session()
		{
			// Initialize the session list
			session = new List<OrderDetail>();
		}
	}
}
