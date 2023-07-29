using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Web;
using WebApplication1.Data;
using WebApplication1.Data.Enum;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IProductRepository _productRepository;
		private readonly IOrderRepository _orderRepository;
		private readonly ApplicationDbContext _context;
		private readonly UserManager<Member> _userManager;

		public HomeController(ILogger<HomeController> logger, IProductRepository productRepository, ApplicationDbContext context, UserManager<Member> userManager, IOrderRepository orderRepository)
		{
			_logger = logger;
			_productRepository = productRepository;
			_context = context;
			_userManager = userManager;
			_orderRepository = orderRepository;
		}

		public async Task<IActionResult> AddToCart(int productId, int quantity)
		{
			// Ürünü veritabanından getir
			var product = await _productRepository.GetById(productId);

			// Sepeti session değişkeninden al veya yeni oluştur
			var cart = Session.session;

			// Sepette aynı üründen varsa miktarını artır, yoksa yeni ekle
			var existingItem = cart.FirstOrDefault(x => x.ProductId == productId);
			if (existingItem != null)
			{
				existingItem.Quantity += quantity;
			}
			else
			{
				cart.Add(new OrderDetail
				{
					ProductId = productId,
					Product = product,
					Quantity = quantity
				});
			}
			Session.session = cart;
		return RedirectToAction ("ShowCart");
		}

		public async Task<IActionResult> RemoveFromCart(int productId)
		{
			// Sepeti session değişkeninden al
			var cart = Session.session;

			// Sepette istenen ürünü bul
			var item = cart.FirstOrDefault(x => x.ProductId == productId);

			// Ürün varsa listeden kaldır
			if (item != null)
			{
				cart.Remove(item);
			}

			// Sepeti session değişkenine geri kaydet
			Session.session = cart;
			return RedirectToAction("ShowCart");
		}

		public IActionResult FilterByCategory(int id)
		{
			ViewBag.ShowCategories = true;
			ViewBag.Categories = _context.Categories.ToList();
			// this line is added
			ViewBag.Products = _context.Products.Where(p => p.CategoryId == id).ToList(); 
			// _context is your database context
			var viewModel = new ProductListViewModel(); 
			viewModel.Products = ViewBag.Products; 
			return View("Index", viewModel);
		}

		public async Task<IActionResult> Index()
		{
			ViewBag.ShowCategories = true;
			ViewBag.Categories = _context.Categories.ToList();
			var productVM = new ProductListViewModel();
			productVM.Categories = ViewBag.Categories;
			productVM.Products = await _productRepository.GetAll();
			return View(productVM);
		}

		public ActionResult ShowCart()
		{
			// Sepeti session değişkeninden al
			var cart = Session.session;

			// Sepet boşsa hata mesajı göster
			if (cart == null || cart.Count == 0)
			{
				ViewBag.Message = "Sepetinizde ürün bulunmamaktadır.";
				return View();
			}

			// Sepeti viewmodel olarak oluştur
			var model = new CartViewModel
			{
				Items = cart
			};

			// Viewmodeli View sayfasına gönder
			return View(model);
		}

        

        public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}