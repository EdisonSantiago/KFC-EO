using System;
using System.Collections.Generic;
using Oulanka.Domain.Models.Respuestas;
using SharpArch.Domain.PersistenceSupport;

namespace Oulanka.Domain.Contracts.Repositories
{
    public interface IRespuestaComentarioRepository : IRepositoryWithTypedId<RespuestaComentario,Guid>
    {
        IList<RespuestaComentario> GetListByEstandar(Guid standardId);
        IList<RespuestaComentario> GetListByRespuesta(Guid respuestaId);
    }
}