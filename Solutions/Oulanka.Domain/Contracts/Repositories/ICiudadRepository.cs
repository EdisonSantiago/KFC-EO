using System;
using Oulanka.Domain.Common;
using Oulanka.Domain.Models.Ubicacion;
using SharpArch.Domain.PersistenceSupport;

namespace Oulanka.Domain.Contracts.Repositories
{
    public interface ICiudadRepository : IRepositoryWithTypedId<Ciudad,Guid>
    {
        PagedList<Ciudad> GetPagedList(int page = 0, int limit = 10);
    }
}