using System;
using Oulanka.Domain.Common;
using Oulanka.Domain.Models.Estandares;
using SharpArch.Domain.PersistenceSupport;

namespace Oulanka.Domain.Contracts.Repositories
{
    public interface ICategoriaRepository : IRepositoryWithTypedId<Categoria, Guid>
    {
        PagedList<Categoria> GetPagedList(int page = 0, int limit = 10);
    }
}