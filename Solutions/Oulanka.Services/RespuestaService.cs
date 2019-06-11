using System;
using System.Collections.Generic;
using System.Linq;
using Oulanka.Domain;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Enums;
using Oulanka.Domain.Models.Respuestas;

namespace Oulanka.Services
{
    public class RespuestaService : IRespuestaService
    {
        private readonly IRespuestaRepository _respuestaRepository;
        private readonly IRespuestaComentarioRepository _comentarioRepository;
        private readonly IEventLogService _eventLogService;

        public RespuestaService(
            IRespuestaRepository respuestaRepository,
            IEventLogService eventLogService,
            IRespuestaComentarioRepository comentarioRepository)
        {
            _respuestaRepository = respuestaRepository;
            _eventLogService = eventLogService;
            _comentarioRepository = comentarioRepository;
        }

        public IList<Respuesta> GetListByEvaluacion(Guid evaluacionId)
        {
            return _respuestaRepository.GetListByEvaluacion(evaluacionId);
        }

        public IList<Respuesta> GetListByEstandard(Guid estandardId)
        {
            return _respuestaRepository.GetListByEstandar(estandardId);
        }

        public Respuesta Get(Guid id)
        {
            return _respuestaRepository.Get(id);
        }

        public ActionConfirmation SaveOrUpdate(Respuesta respuesta)
        {
            if (!respuesta.IsValid()) return ActionConfirmation.CreateFailure("respuesta no es válida");

            try
            {
                _respuestaRepository.SaveOrUpdate(respuesta);
                _respuestaRepository.DbContext.CommitChanges();

                var confirmation = ActionConfirmation.CreateSuccess("Respuesta " + respuesta.Id + " guardada!");
                confirmation.Value = respuesta;

                return confirmation;
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(exception.Message,
                    exception.StackTrace, EventCategory.GuardarObjeto.ToString(), exception, respuesta.ActualizadoPor, EventSource.Sistema);

                return ActionConfirmation.CreateFailure(exception.ToString());
            }
        }

        public ActionConfirmation SaveOrUpdateComentario(RespuestaComentario respuestaComentario)
        {
            if (!respuestaComentario.IsValid()) return ActionConfirmation.CreateFailure("respuesta no es válida");

            try
            {
                _comentarioRepository.SaveOrUpdate(respuestaComentario);
                _comentarioRepository.DbContext.CommitChanges();

                var confirmation = ActionConfirmation.CreateSuccess("Respuesta " + respuestaComentario.Id + " guardada!");
                confirmation.Value = respuestaComentario;

                return confirmation;
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(exception.Message,
                    exception.StackTrace, EventCategory.GuardarObjeto.ToString(), exception, respuestaComentario.ActualizadoPor, EventSource.Sistema);

                return ActionConfirmation.CreateFailure(exception.ToString());
            }
        }

        public IList<RespuestaComentario> GetCommentariosListByRespuesta(Guid respuestaId)
        {
            return _comentarioRepository.GetListByRespuesta(respuestaId);
        }

        public IList<RespuestaComentario> GetCommentariosListByEvaluacion(Guid evaluacionId)
        {
            var comentarios = new List<RespuestaComentario>();
            var respuestas = GetListByEvaluacion(evaluacionId);
            foreach (var respuesta in respuestas)
            {
                var respComentarios = GetCommentariosListByRespuesta(respuesta.Id);
                if (respComentarios.Any())
                {
                    comentarios.AddRange(respComentarios);
                }
            }

            return comentarios;
        }
    }
}