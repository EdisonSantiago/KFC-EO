using System;
using Oulanka.Domain.Common;
using Oulanka.Domain.Models.Ubicacion;
using SharpArch.Domain.PersistenceSupport;

namespace Oulanka.Domain.Contracts.Repositories
{
    public interface IProvinciaRepository : IRepositoryWithTypedId<Provincia,Guid>
    {
        PagedList<Provincia> GetPagedList(int page = 0, int limit = 10);
    }
}