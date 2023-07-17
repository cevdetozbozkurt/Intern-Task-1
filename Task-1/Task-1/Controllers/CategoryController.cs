using Microsoft.AspNetCore.Mvc;
using Task_1.Data;
using Task_1.Interfaces;
using Task_1.Repository;
using Task_1.ViewModels;

namespace Task_1.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ApplicationDbContext context, ICategoryRepository category)
        {
            _context = context;
            _categoryRepository = category;
        }

        public async Task<IActionResult> Index()
		{
			var categoryViewModel = new CategoryViewModel();
			categoryViewModel.Categories = await _categoryRepository.GetAll();

			return View(categoryViewModel);
		}
	}
}
