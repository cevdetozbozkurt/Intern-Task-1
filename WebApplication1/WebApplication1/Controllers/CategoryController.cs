using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Repository;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
	public class CategoryController : Controller
	{

		private readonly ICategoryRepository _categoryRepository;
		private readonly ApplicationDbContext _context;
		public CategoryController(ICategoryRepository categoryRepository, ApplicationDbContext context)
		{
			_categoryRepository = categoryRepository;
			_context = context;
		}

		[HttpGet]
		public IActionResult Create()
		{
			
			var addCategoryViewModel = new AddCategoryViewModel();
			return View(addCategoryViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Create(AddCategoryViewModel categoryVM)
		{
			if (ModelState.IsValid)
			{
				//var result = await _photoService.AddPhotoAsync(clubVM.Image);
				var category = new Category
				{
					Id = categoryVM.Id, 
					Name = categoryVM.Name,
				};
				_categoryRepository.Add(category);
				return RedirectToAction("Categories", "Dashboard");
			}
			else
			{
				ModelState.AddModelError("", "Error");
			}
			return View(categoryVM);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var category = await _categoryRepository.GetById(id);
			if (category == null) { return View("Error"); }
			var editVM = new CategoryEditViewModel()
			{
				Id = category.Id,
				Name = category.Name,
			};
			return View(editVM);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, CategoryEditViewModel editVM)
		{
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("", "Failed to edit profile");
				return View("EditProduct", editVM);
			}
			var category = await _categoryRepository.GetById(id);

			if (category == null) { return View("Error"); }

			category.Name = editVM.Name;

			_categoryRepository.Update(category);

			return RedirectToAction("Categories","Dashboard");
		}
		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			var categoryDetail = await _categoryRepository.GetById(id);
			if (categoryDetail == null) return View("Error");
			return View(categoryDetail);
		}

		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			var categoryDetail = await _categoryRepository.GetById(id);
			if (categoryDetail == null) return View("Error");
			_categoryRepository.Delete(categoryDetail);
			return RedirectToAction("Categories", "Dashboard");
		}
	}
}
