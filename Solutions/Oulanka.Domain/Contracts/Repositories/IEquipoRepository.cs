using System;
using System.Collections.Generic;
using Oulanka.Domain.Common;
using Oulanka.Domain.Models;
using Oulanka.Domain.Models.Locales;
using Oulanka.Domain.Models.Ubicacion;
using SharpArch.Domain.PersistenceSupport;

namespace Oulanka.Domain.Contracts.Repositories
{
    public interface IEquipoRepository : IRepositoryWithTypedId<Equipo, Guid>
    {
        PagedList<Equipo> GetPagedList(int page = 0, int limit = 10);
        IList<Equipo> GetListByTipo(Guid tipoId);
    }
}