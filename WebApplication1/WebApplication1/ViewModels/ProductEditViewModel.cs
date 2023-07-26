using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
	public class ProductEditViewModel
	{

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
		public int CategoryId { get; set; }
		public SelectList? Categories { get; set; }
		public int Stock { get; set; }
    }
}
