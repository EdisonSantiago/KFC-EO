using System;

namespace Oulanka.Providers.Models
{

    [Serializable]
    public class VtOverdraft : IVtOverdraft
    {
        public string AccountNumber { get; set; }
        public string Identification { get; set; }
        public string ClientName { get; set; }
        public string OverdraftType { get; set; }
        public decimal TotalAmmount { get; set; }
        public string Agency { get; set; }
    }
}