using Microsoft.AspNetCore.Mvc;
using System.Net;
using Task_1.Data;
using Task_1.Interfaces;
using Task_1.Models;
using Task_1.ViewModels;

namespace Task_1.Controllers
{
	public class ProductController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly IProductRepository _repository;

		public ProductController(ApplicationDbContext context, IProductRepository productRepository)
		{
			_context = context;
			_repository = productRepository;
		}
		[HttpGet]
		public IActionResult Create()
		{

			var response = new CreateProductViewModel();
			return View(response);
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateProductViewModel productVM)
		{
			

				var product = new Product
				{
					ProductName = productVM.ProductName,
					ProductDescription = productVM.ProductDescription,
					Price = productVM.Price,
					CategoryId = productVM.CategoryId,
					Quantity = productVM.Quantity,
					ProductImageUrl = productVM.ProductImageUrl
				};
				_repository.Add(product);
				return RedirectToAction("Index","Dashboard");
			
			
		}
	}
}
