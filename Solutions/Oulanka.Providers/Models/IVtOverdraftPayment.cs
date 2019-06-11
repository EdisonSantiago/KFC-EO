using System;

namespace Oulanka.Providers.Models
{
    public interface IVtOverdraftPayment
    {
        string AccountNumber { get; set; }
        string ClientName { get; set; }
        string Identification { get; set; }
        DateTime PaymentDate { get; set; }
        decimal PaymentValue { get; set; }
    }
}