namespace Oulanka.Providers.Models
{
    public interface IVtClient
    {
        string ClientType { get; set; }
        string Identification { get; set; }
        string CompleteName { get; set; }
        string AccountNumber { get; set; }
    }
}