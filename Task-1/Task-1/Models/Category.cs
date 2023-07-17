using System.ComponentModel.DataAnnotations;

namespace Task_1.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string? CategoryName { get; set; }

    }
}
