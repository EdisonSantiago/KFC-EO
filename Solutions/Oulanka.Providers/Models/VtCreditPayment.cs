using System;

namespace Oulanka.Providers.Models
{
    public class VtCreditPayment : IVtCreditPayment
    {
        public string Identification { get; set; }
        public string ClientName { get; set; }
        public string Product { get; set; }
        public string OperationNumber { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Dividend { get; set; }
        public decimal Capital { get; set; }
        public decimal Interest { get; set; }
        public decimal Arrears { get; set; }
        public decimal CashPaymentValue { get; set; }
        public decimal CheckPaymentValue { get; set; }
        public string Comments { get; set; }
    }
}