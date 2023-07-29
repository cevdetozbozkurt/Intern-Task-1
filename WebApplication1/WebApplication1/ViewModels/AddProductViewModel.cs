using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication1.ViewModels
{
    public class AddProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
		public IFormFile? Image { get; set; }
		public int CategoryId { get; set; }
        public SelectList? Categories { get; set; }
        public int Stock { get; set; }
    }
}
