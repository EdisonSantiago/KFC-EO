using System;
using Oulanka.Domain.Common;
using Oulanka.Domain.Models;
using Oulanka.Domain.Models.Locales;
using SharpArch.Domain.PersistenceSupport;

namespace Oulanka.Domain.Contracts.Repositories
{
    public interface ITipoLocalRepository : IRepositoryWithTypedId<TipoLocal, Guid>
    {
        PagedList<TipoLocal> GetPagedList(int page = 0, int limit = 10);

    }
}