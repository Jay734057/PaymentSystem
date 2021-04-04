using System.Collections.Generic;

namespace PaymentSystem.Core.Models
{
    public class Account
    {
        public int Id { get; set; }
        public double Balance { get; set; }

        public ICollection<Payment> Payments { get; set; }
    }
}
