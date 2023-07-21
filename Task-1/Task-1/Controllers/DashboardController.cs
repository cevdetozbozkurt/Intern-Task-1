using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Task_1.Data;
using Task_1.Interfaces;
using Task_1.Models;
using Task_1.Repository;
using Task_1.ViewModels;
using Task_1.VıewModels;

namespace Task_1.Controllers
{
    public class DashboardController : Controller
    {

        private readonly IDashboardRepository _repository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<Customer> _userManager;
        private readonly ApplicationDbContext _context;
        public DashboardController(IDashboardRepository dashboardRepository, IHttpContextAccessor httpContextAccessor, UserManager<Customer> userManager,ApplicationDbContext context)
        {
            _repository = dashboardRepository;
            _contextAccessor = httpContextAccessor;
            _userManager = userManager;
            _context = context;
        }

        public string GetCategoryName(Product product)
        {
            var category = _context.Categories.FindAsync(product.CategoryId);
            
            return category.Result.CategoryName;
        }

        [HttpGet("Products")]
		public async Task<IActionResult> Index()
		{

            var products = await _repository.GetAllProducts();

			List<DashboardViewModel> result = new List<DashboardViewModel>();
			foreach (var product in products)
			{
				var dashbaordViewModel = new DashboardViewModel
				{
					Id = product.Id,
					ProductName = product.ProductName,
					ProductDescription = product.ProductDescription,
					ProductImageUrl = product.ProductImageUrl,
					Price = product.Price,
					Quantity = product.Quantity,
					CategoryName = GetCategoryName(product),	
				};
				result.Add(dashbaordViewModel);
			}

			return View(result);
		}





		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			var product = await _repository.GetProductById(id);
			if (product == null) return View("Error");
			return View(product);
		}

		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteUser(int id)
		{
			var product = await _repository.GetProductById(id);
			if (product == null) return View("Error");
			_repository.Delete(product);
			return RedirectToAction("Index");
		}

	}
}

