﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskProject1.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}