using System;
using System.Collections.Generic;
using Oulanka.Domain.Common;
using Oulanka.Domain.Models.Jerarquias;
using SharpArch.Domain.PersistenceSupport;

namespace Oulanka.Domain.Contracts.Repositories
{
    public interface IGerenteRegionalRepository : IRepositoryWithTypedId<GerenteRegional, Guid>
    {
        PagedList<GerenteRegional> GetPagedList(int page = 0, int limit = 10);
        PagedList<GerenteRegional> GetPagedList(Guid parentId, int page = 0, int limit = 10);
        IList<GerenteRegional> GetList();
        IList<GerenteRegional> GetList(Guid parentId);

    }
}