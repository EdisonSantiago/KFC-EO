using System;

namespace Oulanka.Providers.Models
{
    public interface IVtCreditPayment
    {
        string Identification { get; set; }
        string ClientName { get; set; }
        string Product { get; set; }
        string OperationNumber { get; set; }
        DateTime PaymentDate { get; set; }
        string Dividend { get; set; }
        decimal Capital { get; set; }
        decimal Interest { get; set; }
        decimal Arrears { get; set; }
        decimal CashPaymentValue { get; set; }
        decimal CheckPaymentValue { get; set; }
        string Comments { get; set; }
    }
}