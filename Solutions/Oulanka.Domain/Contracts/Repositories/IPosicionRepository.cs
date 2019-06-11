using System;
using Oulanka.Domain.Common;
using Oulanka.Domain.Models.Estandares;
using Oulanka.Domain.Models.Evaluaciones;
using Oulanka.Domain.Models.Personal;
using SharpArch.Domain.PersistenceSupport;

namespace Oulanka.Domain.Contracts.Repositories
{
    public interface IPosicionRepository : IRepositoryWithTypedId<Posicion, Guid>
    {
        PagedList<Posicion> GetPagedList(int page = 0, int limit = 10);
    }
}