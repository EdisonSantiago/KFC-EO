using System;
using System.Collections.Generic;
using Oulanka.Domain.Common;
using Oulanka.Domain.Enums;
using Oulanka.Domain.Models.Estandares;
using SharpArch.Domain.PersistenceSupport;

namespace Oulanka.Domain.Contracts.Repositories
{
    public interface IEstandarRepository : IRepositoryWithTypedId<Estandar, Guid>
    {
        PagedList<Estandar> GetPagedList(int page = 0, int limit = 10);
        PagedList<Estandar> GetByGrupoPagedList(Guid grupoId, int page = 0, int limit = 10);
        PagedList<Estandar> GetByParentPagedList(Guid parentId, int page = 0, int limit = 10);
        PagedList<Estandar> GetByGrupoPagedList(Guid grupoId, TipoEstandar tipoEstandar = TipoEstandar.Contenedor, int page= 0, int limit = 10);
        IList<Estandar> GetByGrupo(Guid grupoId, TipoEstandar tipoEstandar = TipoEstandar.Contenedor);
        IList<Estandar> GetByParent(Guid parentId);
        IList<Estandar> GetPicklist(Guid parentId);
        Estandar GetByCodigo(string codigo);
    }
}