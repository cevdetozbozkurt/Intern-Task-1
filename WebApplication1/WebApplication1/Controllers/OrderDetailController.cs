using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Repository;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
	public class OrderDetailController : Controller
	{

		private readonly IOrderRepository _orderRepository;
		private readonly IOrderDetailRepository _orderDetailRepository;
		private readonly IProductRepository _productRepository;
		private readonly UserManager<Member> _userManager;

		public OrderDetailController(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, IProductRepository productRepository,UserManager<Member> userManager)
		{
			_orderRepository = orderRepository;
			_orderDetailRepository = orderDetailRepository;
			_productRepository = productRepository;
			_userManager = userManager;
		}

		public async Task<ActionResult> Index()
		{
			var orderDetailVM = new OrderDetailLListViewModel()
			{
				OrderDetails = _orderDetailRepository.GetOrderDetailsByMemberId(_userManager.GetUserId(User)),
				Orders = await _orderRepository.GetAllOrders(),
				Products = await _productRepository.GetAll()
			};
			return View(orderDetailVM); 
		}



	}
}
