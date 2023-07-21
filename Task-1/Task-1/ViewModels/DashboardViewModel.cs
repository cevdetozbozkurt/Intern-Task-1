namespace Task_1.ViewModels
{
	public class DashboardViewModel
	{
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public string? ProductImageUrl { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string CategoryName { get; set; }
    }
}
