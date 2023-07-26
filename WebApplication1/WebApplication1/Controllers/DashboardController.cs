using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Repository;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
		private readonly ApplicationDbContext _context;
        private readonly ICategoryRepository _categoryRepository;

		public DashboardController(IProductRepository productRepository, IUserRepository userRepository, ApplicationDbContext context, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
            _context = context;
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
			
			return View();
        }

        public async Task<IActionResult> Categories()
        {
            var categoryVM = new CategoryListViewModel();
			categoryVM.Categories = await _categoryRepository.GetAll();
			return View(categoryVM);
		}

        public async Task<IActionResult> Products()
        {
			
			var productVM = new ProductListViewModel();
            productVM.Products = await _productRepository.GetAll();
            return View(productVM);
        }

        public async Task<IActionResult> Users()
        {
			
			var users = await _userRepository.GetAllUsers();
            List<UserListViewModel> result = new List<UserListViewModel>();
            foreach (var user in users)
            {
                var userViewModel = new UserListViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Address = user.Address,
                };
                result.Add(userViewModel);
            }

            return View(result);
        }

    }
}
