using Microsoft.AspNetCore.Mvc;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.ViewModels;
using Microsoft.AspNetCore.Identity;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
	public class OrderController : Controller
	{

		private readonly IOrderRepository _orderRepository;
		private readonly IOrderDetailRepository _orderDetailRepository;
		private readonly IMemberRepository _memberRepository;
		private readonly IProductRepository _productRepository;
		private readonly UserManager<Member> _userManager;

		public OrderController(IOrderRepository orderRepository, IMemberRepository member, UserManager<Member> userManager, IOrderDetailRepository orderDetailRepository, IProductRepository productRepository)
		{
			_orderRepository = orderRepository;
			_memberRepository = member;
			_userManager = userManager;
			_orderDetailRepository = orderDetailRepository;
			_productRepository = productRepository;

		}

		public async Task<IActionResult> Index()
		{
			var orderVM = new OrderListViewModel();
			orderVM.Orders = _orderRepository.GetOrdersByMemberId(_userManager.GetUserId(User));
			orderVM.Members = await _memberRepository.GetAllMembers();
			return View(orderVM);
		}

		public async Task<IActionResult> Detail(int id)
		{

			var order = _orderRepository.GetOrderById(id);
			if (order == null)
			{
				return View("Error");
			}
			// buraya DetailViewModel hazirlanip detailVM View e gonderilecek
			return View(order);
		}

		// GET: Order/Create
		public async Task<IActionResult> Create()
		{
			var addOrderVM = new AddOrderViewModel();
			return View(addOrderVM);
		}

		[HttpPost]
		public async Task<IActionResult> Create(AddOrderViewModel orderVM)
		{
			if (ModelState.IsValid)
			{
				var userId = _userManager.GetUserId(User);
				var order = new Order
				{
					Date = DateTime.Now,
					Total = orderVM.Total,
					Status = orderVM.Status,
					MemberId = userId,
				};
				_orderRepository.Add(order);
				return RedirectToAction("Index", "Home");
			}
			return RedirectToAction("Index", "Home");
		}

		// GET: Order/Edit/5
		public ActionResult Edit(int id)
		{
			var order = _orderRepository.GetOrderById(id); // Id'ye göre siparişi getirme
			if (order == null) // Sipariş bulunamazsa
			{
				return View("Error"); // 404 hatası döndürme
			}
			var editOrderVM = new EditOrderViewModel()
			{
				Id = order.Id, // Sipariş id'sini order'dan alıp view model'e atama
				Status = (Data.Enum.OrderStatus)order.Status,
				Date = order.Result.Date,
				Total = order.Result.Total,

			};
			return View(editOrderVM); // View model'i view'a gönderme
		}

		// POST: Order/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(EditOrderViewModel editOrderVM)
		{
			if (ModelState.IsValid) // Model doğrulaması geçerliyse
			{
				Order order = new Order()
				{
					Id = editOrderVM.Id, 
					Date = editOrderVM.Date, 
					Total = editOrderVM.Total, 
					Status = editOrderVM.Status, 
					MemberId = _userManager.GetUserId(User),
				}; 
				_orderRepository.Update(order); // Siparişi güncelleme
				return RedirectToAction("Index"); // Index action'a yönlendirme
			}
			return View(editOrderVM); // Model doğrulaması geçerli değilse, view model'i view'a gönderme
		}
		public ActionResult Delete(int id)
		{
			var order = _orderRepository.GetOrderById(id); // Id'ye göre siparişi getirme
			if (order == null) // Sipariş bulunamazsa
			{
				return View("Error"); // 404 hatası döndürme
			}
			return View(order); // Siparişi view'a gönderme
		}

		// POST: Order/Delete/5
		[HttpPost, ActionName("Delete")]
		public async Task<ActionResult> DeleteConfirmed(int id)
		{
			var order = await _orderRepository.GetOrderById(id);
			_orderRepository.Delete(order); // Siparişi silme
			return RedirectToAction("Index","Order"); // Index action'a yönlendirme
		}

	}
}
