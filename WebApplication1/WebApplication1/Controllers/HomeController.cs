using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IProductRepository _productRepository;
		private readonly ApplicationDbContext _context;

		public HomeController(ILogger<HomeController> logger, IProductRepository productRepository, ApplicationDbContext context)
		{
			_logger = logger;
			_productRepository = productRepository;
			_context = context;
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