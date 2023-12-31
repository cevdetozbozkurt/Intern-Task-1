﻿using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly ApplicationDbContext _context;

		public CategoryRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public bool Add(Category category)
		{
			_context.Add(category);
			return Save();
		}

		public bool Delete(Category category)
		{
			_context.Remove(category);
			return Save();
		}

		public async Task<IEnumerable<Category>> GetAll()
		{
			return await _context.Categories.ToListAsync();
		}

		public async Task<Category> GetById(int id)
		{
			return await _context.Categories.FindAsync(id);
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public bool Update(Category category)
		{
			_context.Update(category);
			return Save();
		}

		public bool UpdateName(Category category)
		{
			_context.Attach(category);
			return Save();
		}
	}
}
