using System;
using System.Collections.Generic;
using Oulanka.Domain.Common;
using Oulanka.Domain.Models.Jerarquias;
using SharpArch.Domain.PersistenceSupport;

namespace Oulanka.Domain.Contracts.Repositories
{
    public interface IGerenteNacionalRepository : IRepositoryWithTypedId<GerenteNacional, Guid>
    {
        PagedList<GerenteNacional> GetPagedList(int page = 0, int limit = 10);
        PagedList<GerenteNacional> GetPagedList(Guid parentId, int page = 0, int limit = 10);
        PagedList<GerenteNacional> GetByCadenaPagedList(Guid cadenaId, int page = 0, int limit = 10);
        IList<GerenteNacional> GetList();
        IList<GerenteNacional> GetList(Guid parentId);
        IList<GerenteNacional> GetListByCadena(Guid cadenaId);

    }
}