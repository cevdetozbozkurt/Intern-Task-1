using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Task_1.Models
{
    public class Wallet
    {
        [Key]
        public int Id { get; set; }
        [DefaultValue(0)]
        public double WalletBalance { get; set; }
    }
}
