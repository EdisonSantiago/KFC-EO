using System;
using System.Collections.Generic;
using Oulanka.Domain.Models.Respuestas;

namespace Oulanka.Domain.Contracts.Services
{
    public interface IRespuestaService
    {
        IList<Respuesta> GetListByEvaluacion(Guid evaluacionId);
        IList<Respuesta> GetListByEstandard(Guid estandardId);
        Respuesta Get(Guid id);
        ActionConfirmation SaveOrUpdate(Respuesta respuesta);
        ActionConfirmation SaveOrUpdateComentario(RespuestaComentario respuestaComentario);
        IList<RespuestaComentario> GetCommentariosListByRespuesta(Guid respuestaId);
        IList<RespuestaComentario> GetCommentariosListByEvaluacion(Guid evaluacionId);
    }
}