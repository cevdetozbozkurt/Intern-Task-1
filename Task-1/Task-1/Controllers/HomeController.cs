using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Task_1.Interfaces;
using Task_1.Models;
using Task_1.VıewModels;

namespace Task_1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public HomeController(ILogger<HomeController> logger, IProductRepository productRepository)
        {
            _logger = logger;
			_productRepository = productRepository;
		}

        public async Task<IActionResult> Index()
        {
            var homeViewModel = new HomeViewModel();

            homeViewModel.Products = await _productRepository.GetAll();
            return View(homeViewModel);
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