using System;
using Oulanka.Domain.Common;
using Oulanka.Domain.Models;
using Oulanka.Domain.Models.Locales;
using Oulanka.Domain.Models.Ubicacion;
using SharpArch.Domain.PersistenceSupport;

namespace Oulanka.Domain.Contracts.Repositories
{
    public interface ITipoEquipoRepository : IRepositoryWithTypedId<TipoEquipo, Guid>
    {
        PagedList<TipoEquipo> GetPagedList(int page = 0, int limit = 10);
    }
}