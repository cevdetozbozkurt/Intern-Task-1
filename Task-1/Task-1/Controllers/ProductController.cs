using Microsoft.AspNetCore.Mvc;
using Task_1.Data;

namespace Task_1.Controllers
{
	public class ProductController : Controller
	{
		private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
		{
			var produtcs = _context.Products.ToList();
			return View();
		}
	}
}
