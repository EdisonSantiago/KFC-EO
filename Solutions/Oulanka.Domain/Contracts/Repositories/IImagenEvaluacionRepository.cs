using System;
using Oulanka.Domain.Common;
using Oulanka.Domain.Models;
using Oulanka.Domain.Models.Evaluaciones;
using SharpArch.Domain.PersistenceSupport;

namespace Oulanka.Domain.Contracts.Repositories
{
    public interface IImagenEvaluacionRepository : IRepository<ImagenEvaluacion>
    {
        PagedList<ImagenEvaluacion> GetPagedListByEvaluacion(Guid evaluationId, int page = 0, int limit = 10);
    }
}