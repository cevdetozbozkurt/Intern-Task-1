using Microsoft.AspNetCore.Mvc;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.ViewModels;
using Microsoft.AspNetCore.Identity;
using WebApplication1.Repository;
using WebApplication1.Data.Enum;
using WebApplication1.Data;

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

		public async Task<IActionResult> OrderConfirmation()
		{
			return View();
		}

		public async Task<IActionResult> Orders()
		{
			var model = _orderRepository.GetOrdersByMemberId(_userManager.GetUserId(User));
			return View(model);
		}

		public async Task<IActionResult> OrderDetails(int id)
		{
			var order = await _orderRepository.GetOrderById(id);
			var products = new List<Product>();
			var quantities = new List<int>();
			var member = await _memberRepository.GetMemberNoTrackingById(order.MemberId);
			foreach (var item in order.OrderDetails)
			{
				products.Add(await _productRepository.GetById(item.ProductId));
				quantities.Add(item.Quantity);
			}

			var orderDetailVM = new AddOrderDetailViewModel()
			{
				Member = member,
				Order = order,
				OrderId = order.Id,
				Products = products,
				Quantity = quantities,
			};


			return View(orderDetailVM);
		}

		public async Task<ActionResult> Checkout()
		{
			// Sepeti session değişkeninden al
			var cart = Session.session;

			// Sepet boşsa hata mesajı göster
			if (cart == null || cart.Count == 0)
			{
				ViewBag.Message = "Sepetinizde ürün bulunmamaktadır.";
				return View();
			}

			var orderDetail = new OrderDetail();
			var member = await _userManager.GetUserAsync(User);

			// Yeni bir sipariş nesnesi oluştur
			var order = new Order
			{
				Date = DateTime.Now, // Sipariş tarihi
				Total = cart.Sum(x => x.Product.Price * x.Quantity), // Sipariş toplamı
				Status = OrderStatus.Hazırlanıyor, // Sipariş durumu
				MemberId = member.Id, // Siparişi veren üye idsi
				Member = member,
				OrderDetails = new List<OrderDetail>() // Sipariş detayları
			};

			// Siparişi veritabanına kaydet
			_orderRepository.Add(order);

			foreach (var odetail in cart)
			{
				orderDetail = odetail;
				orderDetail.Order = order;
				order.OrderDetails.Add(orderDetail);
			}

			_orderRepository.Update(order);


			var addOrderVM = new AddOrderViewModel()
			{
				Date = order.Date,
				Status = order.Status,
				Total = order.Total,
			};

			// Sepeti session değişkeninden sil
			Session.session = new List<OrderDetail>();

			// Sipariş onay sayfasına yönlendir
			return View(addOrderVM);
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
		public async Task<ActionResult> Edit(int id)
		{
			var order = await _orderRepository.GetOrderById(id);  // Id'ye göre siparişi getirme
			var member = await _memberRepository.GetMemberById(order.MemberId);
			var orderDetail = _orderDetailRepository.GetOrderDetailsByMemberId(member.Id);
			if (order == null) // Sipariş bulunamazsa
			{
				return View("Error"); // 404 hatası döndürme
			}
			var editOrderVM = new EditOrderViewModel()
			{
				Id = order.Id, // Sipariş id'sini order'dan alıp view model'e atama
				Status = order.Status,
				Date = order.Date,
				Total = order.Total,
				OrderDetails = orderDetail,
				Member = member,
			};
			return View(editOrderVM); // View model'i view'a gönderme
		}

		// POST: Order/Edit/5
		[HttpPost]
		public async Task<ActionResult> Edit(EditOrderViewModel editOrderVM)
		{
			var order = await _orderRepository.GetOrderById(editOrderVM.Id);
			var member = await _memberRepository.GetMemberById(order.MemberId);
			order.Member = member;
			if (ModelState.IsValid) // Model doğrulaması geçerliyse
			{
				order.Date = order.Date;
				order.Total = editOrderVM.Total;
				order.Status = editOrderVM.Status;
				order.MemberId = order.MemberId;

				_orderRepository.Update(order);
				foreach (var orderDetail in order.OrderDetails)
				{
					var temp = 0;
				}
				
				
				// Siparişi güncelleme
				return RedirectToAction("Orders", "Dashboard"); // Index action'a yönlendirme
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
			return RedirectToAction("Index", "Order"); // Index action'a yönlendirme
		}

	}
}
