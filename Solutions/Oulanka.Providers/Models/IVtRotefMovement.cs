namespace Oulanka.Providers.Models
{
    public interface IVtRotefMovement
    {
        string TipoIdCliente { get; set; }
        string IdCliente { get; set; }
        string RazonSocial { get; set; }
        string PaisNacionalidad { get; set; }
        string Dir { get; set; }
        string DirProv { get; set; }
        string DirCanton { get; set; }
        string TipoProducto { get; set; }
        string NumProducto { get; set; }
        decimal ValDebito { get; set; }
        decimal ValCredito { get; set; }
        decimal ValEfectivo { get; set; }
        decimal ValCheque { get; set; }
        decimal ValTotal { get; set; }
        string TipoOperacion { get; set; }
        string CodMoneda { get; set; }
        string PaisTrx { get; set; }
        string RazonSocialTrx { get; set; }
        string IfiTrx { get; set; }
        string NumProductoTrx { get; set; }
        int NumOperaciones { get; set; }

    }
}