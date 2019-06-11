using System;
using Oulanka.Domain.Dtos;

namespace Oulanka.Web.Core.ViewModels
{
    public class EstandarViewModel
    {
        public Guid EstandarId { get; set; }
        public string CodigoEstandar { get; set; }
        public EstandarDto Dto { get; set; }
    }
}