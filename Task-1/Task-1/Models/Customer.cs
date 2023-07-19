using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task_1.Models
{
    public class Customer : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        [ForeignKey("Product")]
        public int? ProductId { get; set; }
        public int? ProductQuantity { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        [ForeignKey("Wallet")]
        public int WalletId { get; set; }
        public Wallet Wallet { get; set; }
        [ForeignKey("Order")]
        public int? OrderId { get; set; }
        public ICollection<Order>? Orders{ get; set; }
        [ForeignKey("OrderItem")]
        public int? OrderItemId { get; set; }
        public OrderItem? OrderItem { get; set; }
    }
}
