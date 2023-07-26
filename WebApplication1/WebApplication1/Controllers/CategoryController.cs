using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Repository;

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

		private bool CategoryExists(int id)
		{
			return _context.Categories.Any(e => e.Id == id); // _context is your database context
		}

		[HttpPost]
		public async Task<IActionResult> Create(Category category)
		{
			if (ModelState.IsValid)
			{
				_categoryRepository.Add(category);
				return RedirectToAction("Index");
			}
			return View(category);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, Category category)
		{
			if (id != category.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_categoryRepository.Update(category);
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!CategoryExists(category.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction("Index");
			}
			return View(category);
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
