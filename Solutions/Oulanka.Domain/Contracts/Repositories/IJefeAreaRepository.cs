using System;
using System.Collections.Generic;
using Oulanka.Domain.Common;
using Oulanka.Domain.Models.Jerarquias;
using SharpArch.Domain.PersistenceSupport;

namespace Oulanka.Domain.Contracts.Repositories
{
    public interface IJefeAreaRepository : IRepositoryWithTypedId<JefeArea, Guid>
    {
        PagedList<JefeArea> GetPagedList(int page = 0, int limit = 10);
        PagedList<JefeArea> GetPagedList(Guid parentId, int page = 0, int limit = 10);
        PagedList<JefeArea> GetPagedList(Guid[] parentIds, int page = 0, int limit = 10);
        IList<JefeArea> GetList();
        IList<JefeArea> GetList(Guid parentId);

    }
}