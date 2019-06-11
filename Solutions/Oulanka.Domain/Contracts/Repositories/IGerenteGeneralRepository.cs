using System;
using System.Collections.Generic;
using Oulanka.Domain.Common;
using Oulanka.Domain.Models.Jerarquias;
using SharpArch.Domain.PersistenceSupport;

namespace Oulanka.Domain.Contracts.Repositories
{
    public interface IGerenteGeneralRepository : IRepositoryWithTypedId<GerenteGeneral, Guid>
    {
        PagedList<GerenteGeneral> GetPagedList(int page = 0, int limit = 10);
        IList<GerenteGeneral> GetList();
    }
}