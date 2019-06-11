using System;
using Oulanka.Domain.Common;
using Oulanka.Domain.Models;
using Oulanka.Domain.Models.Ubicacion;
using SharpArch.Domain.PersistenceSupport;

namespace Oulanka.Domain.Contracts.Repositories
{
    public interface ICadenaRepository : IRepositoryWithTypedId<Cadena, Guid>
    {
        PagedList<Cadena> GetPagedList(int page = 0, int limit = 10);
    }
}