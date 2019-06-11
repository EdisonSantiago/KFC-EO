using System;
using Oulanka.Domain.Common;
using Oulanka.Domain.Models.Estandares;
using Oulanka.Domain.Models.Evaluaciones;
using SharpArch.Domain.PersistenceSupport;

namespace Oulanka.Domain.Contracts.Repositories
{
    public interface ITipoEvaluacionRepository : IRepositoryWithTypedId<TipoEvaluacion, Guid>
    {
        PagedList<TipoEvaluacion> GetPagedList(int page = 0, int limit = 10);
    }
}