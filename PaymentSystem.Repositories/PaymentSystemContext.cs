using Microsoft.EntityFrameworkCore;
using PaymentSystem.Core.Models;

namespace PaymentSystem.Repositories
{
    public class PaymentSystemContext : DbContext
    {
        public PaymentSystemContext(DbContextOptions<PaymentSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }

        public virtual DbSet<Payment> Payments { get; set; }
    }
}
