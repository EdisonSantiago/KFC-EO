namespace Oulanka.Providers.Models
{
    public interface IVtOverdraft
    {
        string AccountNumber { get; set; }
        string Identification { get; set; }
        string ClientName { get; set; }
        string OverdraftType { get; set; }
        decimal TotalAmmount { get; set; }
        string Agency { get; set; }
    }
}