using System;
using Oulanka.Domain.Common;
using Oulanka.Domain.Models;
using Oulanka.Domain.Models.Security;
using Oulanka.Domain.Models.Ubicacion;
using SharpArch.Domain.PersistenceSupport;

namespace Oulanka.Domain.Contracts.Repositories
{
    public interface IAplicacionClienteRepository : IRepositoryWithTypedId<AplicacionCliente, Guid>
    {

    }
}