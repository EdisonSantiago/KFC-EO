using System;
using System.Collections.Generic;
using Oulanka.Domain.Common;
using Oulanka.Domain.Models;
using Oulanka.Domain.Models.Locales;
using SharpArch.Domain.PersistenceSupport;

namespace Oulanka.Domain.Contracts.Repositories
{
    public interface ILocalRepository : IRepositoryWithTypedId<Local, Guid>
    {
        PagedList<Local> GetPagedList(int page = 0, int limit = 10);

        IList<Local> GetListByCadena(Guid cadenaId);
        PagedList<Local> GetPagedList(Guid cadenaId, int page, int limit);
        PagedList<Local> GetPagedList(Guid cadenaId, Guid statusId, int page, int limit);
        Local GetByCode(string code);
    }
}