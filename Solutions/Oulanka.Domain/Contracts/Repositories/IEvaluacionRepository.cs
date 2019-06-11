using System;
using System.Collections.Generic;
using Oulanka.Domain.Common;
using Oulanka.Domain.Dtos;
using Oulanka.Domain.Models.Estandares;
using Oulanka.Domain.Models.Evaluaciones;
using SharpArch.Domain.PersistenceSupport;

namespace Oulanka.Domain.Contracts.Repositories
{
    public interface IEvaluacionRepository : IRepositoryWithTypedId<Evaluacion, Guid>
    {
        PagedList<Evaluacion> GetPagedList(int page = 0, int limit = 10);
        PagedList<EvaluacionDto> GetDtoByCadenaPagedList(Guid cadenaId, int page = 0, int limit = 10);
        PagedList<EvaluacionDto> GetDtoByLocalPagedList(Guid localId, int page = 0, int limit = 10);
    }
}