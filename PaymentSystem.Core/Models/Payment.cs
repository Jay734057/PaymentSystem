using System;
using System.Text.Json.Serialization;

namespace PaymentSystem.Core.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public PaymentStatus Status { get; set; }
        public string Message { get; set; }
        public int AccountId { get; set; }
        [JsonIgnore]
        public Account Account { get; set; }
    }

    public enum PaymentStatus
    {
        Pending,
        Complete,
        Cancelled,
        Rejected
    }
}
