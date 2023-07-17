using System.ComponentModel.DataAnnotations;

namespace Task_1.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        public Product Product { get; set; }
        public double ProductPrice { get; set; }
    }
}
