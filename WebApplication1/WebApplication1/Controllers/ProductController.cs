using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Repository;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
	public class ProductController : Controller
	{

		private readonly IProductRepository _productRepository;
		private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context,IProductRepository productRepository)
        {
            _context = context;
			_productRepository = productRepository;
        }

        public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> EditProduct(int id)
		{
			var product = await _productRepository.GetById(id);

			if (product == null)
			{
				return View("Error");
			}
			var categories = _context.Categories.ToList();
			var editMV = new ProductEditViewModel()
			{
				Id = product.Id,
				Name = product.Name,
				Description = product.Description,
				Price = product.Price,
				Stock = product.Stock,
				Image = product.Image,
				Categories = new SelectList(categories, "Id","Name"),
			};
			return View(editMV);
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> EditProduct(ProductEditViewModel editVM, int id)
		{
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("", "Failed to edit profile");
				return View("EditProduct", editVM);
			}

			var product = await _productRepository.GetById(id);

			if (product == null)
			{
				return View("Error");
			}
			product.Name = editVM.Name;
			product.Description = editVM.Description;
			product.Price = editVM.Price;
			product.Stock = editVM.Stock;
			product.Image = editVM.Image;
			product.CategoryId = editVM.CategoryId;
			product.Category = _context.Categories.Find(editVM.CategoryId);
			_productRepository.Update(product);

			return RedirectToAction("Products", "Dashboard");
		}

		[HttpGet]
        public IActionResult Create()
        {
			var categories = _context.Categories.ToList();
			var addProductViewModel = new AddProductViewModel()
			{
				Categories = new SelectList(categories, "Id", "Name"),
			};
            return View(addProductViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {
                //var result = await _photoService.AddPhotoAsync(clubVM.Image);
                var product = new Product
                {
					Id = productVM.Id,
                    Name = productVM.Name,
                    Description = productVM.Description,
                    Image = productVM.Image,
                    Stock = productVM.Stock,
                    Price = productVM.Price,
					CategoryId = productVM.CategoryId,
					Category = _context.Categories.Find(productVM.CategoryId),
                };
                _productRepository.Add(product);
                return RedirectToAction("Products","Dashboard");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(productVM);
        }


        [HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			var productDetail = await _productRepository.GetById(id);
			if (productDetail == null) return View("Error");
			return View(productDetail);
		}

		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			var productDetail = await _productRepository.GetById(id);
			if (productDetail == null) return View("Error");
			_productRepository.Delete(productDetail);
			return RedirectToAction("Users", "Dashboard");
		}
	}
}
