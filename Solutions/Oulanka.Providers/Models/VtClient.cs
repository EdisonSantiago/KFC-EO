namespace Oulanka.Providers.Models
{
    public class VtClient : IVtClient
    {
        public string ClientType { get; set; }
        public string Identification { get; set; }
        public string CompleteName { get; set; }
        public string AccountNumber { get; set; }
    }
}