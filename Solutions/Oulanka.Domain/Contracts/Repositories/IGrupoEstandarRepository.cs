using System;
using System.Collections.Generic;
using Oulanka.Domain.Common;
using Oulanka.Domain.Models.Estandares;
using SharpArch.Domain.PersistenceSupport;

namespace Oulanka.Domain.Contracts.Repositories
{
    public interface IGrupoEstandarRepository : IRepositoryWithTypedId<GrupoEstandar,Guid>
    {
        PagedList<GrupoEstandar> GetPagedList(int page = 0, int limit = 10);
        GrupoEstandar GetByCodigo(string codigo);
        IList<GrupoEstandar> GetByTipoLocalList(Guid tipoLocalId);
    }
}