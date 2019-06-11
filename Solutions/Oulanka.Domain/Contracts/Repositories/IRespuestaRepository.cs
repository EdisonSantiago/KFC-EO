using System;
using System.Collections.Generic;
using Oulanka.Domain.Models.Respuestas;
using SharpArch.Domain.PersistenceSupport;

namespace Oulanka.Domain.Contracts.Repositories
{
    public interface IRespuestaRepository : IRepositoryWithTypedId<Respuesta,Guid>
    {
        IList<Respuesta> GetListByEvaluacion(Guid evaluacionId);
        IList<Respuesta> GetListByEstandar(Guid standardId);
    }
}