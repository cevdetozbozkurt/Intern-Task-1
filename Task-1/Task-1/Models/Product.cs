using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task_1.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string ProductName{ get; set; }
        public string ProductImageUrl { get; set; }
        public string? ProductDescription { get; set; }
        [DefaultValue(0)]
        public int Quantity { get; set; }
        [DefaultValue(0)]
        public double Price { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
    }
}
