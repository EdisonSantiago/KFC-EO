using System;

namespace Oulanka.Providers.Models
{
    public enum VtRotefMovementType
    {
        FromAccounts,
        FromCredits
    }

    [Serializable]
    public class VtRotefMovement : IVtRotefMovement
    {
        public string TipoIdCliente { get; set; }
        public string IdCliente { get; set; }
        public string RazonSocial { get; set; }
        public string PaisNacionalidad { get; set; }
        public string Dir { get; set; }
        public string DirProv { get; set; }
        public string DirCanton { get; set; }
        public string TipoProducto { get; set; }
        public string NumProducto { get; set; }
        public decimal ValDebito { get; set; }
        public decimal ValCredito { get; set; }
        public decimal ValEfectivo { get; set; }
        public decimal ValCheque { get; set; }
        public decimal ValTotal { get; set; }
        public string TipoOperacion { get; set; }
        public string CodMoneda { get; set; }
        public string PaisTrx { get; set; }
        public string RazonSocialTrx { get; set; }
        public string IfiTrx { get; set; }
        public string NumProductoTrx { get; set; }
        public int NumOperaciones { get; set; }
    }
}