using Microsoft.AspNetCore.Mvc;
using Task_1.Data;
using Task_1.Interfaces;
using Task_1.ViewModels;

namespace Task_1.Controllers
{
	public class OrderController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly IOrderRepository _repository;

        public OrderController(ApplicationDbContext context, IOrderRepository repository)
        {
			_context = context;
			_repository = repository;
        }

        public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> Add(string id)
		{
			var orderViewModel = new OrderViewModel();
			orderViewModel.Product = (ICollection<Models.Product>)await _repository.GetAllProducts();
			return View(orderViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Add(int id)
		{
			_repository.Add(await _context.Products.FindAsync(id));
			return RedirectToAction("Index","Order");
		}
	}
}
