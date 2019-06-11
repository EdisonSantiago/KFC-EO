using System;

namespace Oulanka.Providers.Models
{
    public class VtOverdraftPayment : IVtOverdraftPayment
    {
        public string AccountNumber { get; set; }
        public string ClientName { get; set; }
        public string Identification { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentValue { get; set; }
    }
}