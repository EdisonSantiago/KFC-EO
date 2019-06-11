using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Oulanka.Domain;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Dtos;
using Oulanka.Domain.Enums;
using Oulanka.Domain.Models;
using Oulanka.Domain.Models.Estandares;
using Oulanka.Domain.Models.Evaluaciones;
using Oulanka.Domain.Models.Personal;
using Oulanka.Domain.Models.Respuestas;

namespace Oulanka.Services
{
    public class EvaluacionService : IEvaluacionService
    {
        private readonly IEvaluacionRepository _evaluacionRepository;
        private readonly IEstandarService _estandarService;
        private readonly IRespuestaService _respuestaService;
        private readonly ITipoEvaluacionRepository _tipoEvaluacionRepository;
        private readonly IPosicionRepository _posicionRepository;
        private readonly IEventLogService _eventLogService;
        private readonly IStatusService _statusService;
        private readonly IImagenEvaluacionRepository _imagenEvaluacionRepository;

        public EvaluacionService(
            IEvaluacionRepository evaluacionRepository,
            ITipoEvaluacionRepository tipoEvaluacionRepository,
            IEventLogService eventLogService,
            IPosicionRepository posicionRepository,
            IStatusService statusService,
            IImagenEvaluacionRepository imagenEvaluacionRepository,
            IRespuestaService respuestaService, IEstandarService estandarService)
        {
            _evaluacionRepository = evaluacionRepository;
            _tipoEvaluacionRepository = tipoEvaluacionRepository;
            _eventLogService = eventLogService;
            _posicionRepository = posicionRepository;
            _statusService = statusService;
            _imagenEvaluacionRepository = imagenEvaluacionRepository;
            _respuestaService = respuestaService;
            _estandarService = estandarService;
        }


        public PagedList<Evaluacion> GetPagedList(int page = 0, int limit = 10)
        {
            return _evaluacionRepository.GetPagedList(page, limit);
        }

        public PagedList<EvaluacionDto> GetByCadenaPagedList(Guid cadenaId, int page = 0, int limit = 10)
        {
            return _evaluacionRepository.GetDtoByCadenaPagedList(cadenaId, page, limit);
        }

        public PagedList<EvaluacionDto> GetByLocalPagedList(Guid localId, int page = 0, int limit = 10)
        {
            return _evaluacionRepository.GetDtoByLocalPagedList(localId, page, limit);
        }

        public IList<Evaluacion> GetList()
        {
            return _evaluacionRepository.GetAll();
        }

        public Evaluacion Get(Guid id)
        {
            return _evaluacionRepository.Get(id);
        }

        public ActionConfirmation SaveOrUpdate(Evaluacion evaluacion)
        {
            if (!evaluacion.IsValid()) return ActionConfirmation.CreateFailure("evaluación no es válida");

            try
            {
                _evaluacionRepository.SaveOrUpdate(evaluacion);
                _evaluacionRepository.DbContext.CommitChanges();

                var confirmation = ActionConfirmation.CreateSuccess("Evaluacion " + evaluacion.Id + " guardada!");
                confirmation.Value = evaluacion;

                return confirmation;
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(exception.Message,
                    exception.StackTrace, EventCategory.GuardarObjeto.ToString(), exception, evaluacion.ActualizadoPor, EventSource.Sistema);

                return ActionConfirmation.CreateFailure(exception.ToString());
            }
        }

        public ActionConfirmation Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        #region Tipo Evaluacion

        public PagedList<TipoEvaluacion> GetTipoEvaluacionPagedList(int page = 0, int limit = 10)
        {
            return _tipoEvaluacionRepository.GetPagedList(page, limit);
        }

        public IList<TipoEvaluacion> GetTipoEvaluacionList()
        {
            return _tipoEvaluacionRepository.GetAll();
        }

        public IList<TipoEvaluacion> GetTipoEvaluacionList(bool onlineOnly)
        {
            var onlineEstado = _statusService.Online();
            return GetTipoEvaluacionList().Where(x => x.Estado == onlineEstado).ToList();
        }

        public TipoEvaluacion GetTipoEvaluacion(Guid id)
        {
            return _tipoEvaluacionRepository.Get(id);
        }

        public ActionConfirmation SaveOrUpdateTipoEvaluacion(TipoEvaluacion tipoEvaluacion)
        {
            if (!tipoEvaluacion.IsValid()) return ActionConfirmation.CreateFailure("item no es válido");

            try
            {
                _tipoEvaluacionRepository.SaveOrUpdate(tipoEvaluacion);
                _tipoEvaluacionRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("saved ok");
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(exception.Message,
                    exception.StackTrace, EventCategory.GuardarObjeto.ToString(), exception, tipoEvaluacion.ActualizadoPor, EventSource.Sistema);

                return ActionConfirmation.CreateFailure(exception.ToString());
            }

        }


        public ActionConfirmation DeleteTipoEvaluacion(Guid id)
        {
            var item = _tipoEvaluacionRepository.Get(id);
            if (item == null) return ActionConfirmation.CreateFailure("tipo equipo no existe");

            try
            {
                _tipoEvaluacionRepository.Delete(item.Id);
                _tipoEvaluacionRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("Delete OK (" + item.Nombre + ")");
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(exception.Message,
                    exception.StackTrace, EventCategory.EliminarObjeto.ToString(), exception, item.ActualizadoPor, EventSource.Sistema);

                return ActionConfirmation.CreateFailure(exception.ToString());
            }

        }

        public PagedList<Posicion> GetPosicionPagedList(int page = 0, int limit = 10)
        {
            return _posicionRepository.GetPagedList(page, limit);
        }

        public Posicion GetPosicion(Guid id)
        {
            return _posicionRepository.Get(id);
        }

        public IList<Posicion> GetPosicionList()
        {
            return _posicionRepository.GetAll();
        }

        public IList<Posicion> GetPosicionList(bool onlineOnly)
        {
            var onlineEstado = _statusService.Online();
            return _posicionRepository.GetAll().Where(x => x.Estado == onlineEstado).ToList();
        }

        public IList<Posicion> GetPosicionList(Guid cadena, bool onlineOnly)
        {
            var onlineEstado = _statusService.Online();
            return _posicionRepository.GetAll().Where(
                x =>
                    x.Cadena.Id == cadena &&
                    Equals(x.Estado, onlineEstado)
                ).ToList();
        }

        public ActionConfirmation SaveOrUpdatePosicion(Posicion posicion)
        {
            if (!posicion.IsValid()) return ActionConfirmation.CreateFailure("item no es válido");

            try
            {
                _posicionRepository.SaveOrUpdate(posicion);
                _posicionRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("saved ok");
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(exception.Message,
                    exception.StackTrace, EventCategory.GuardarObjeto.ToString(), exception, posicion.ActualizadoPor, EventSource.Sistema);

                return ActionConfirmation.CreateFailure(exception.ToString());
            }
        }

        public ActionConfirmation DeletePosicion(Guid id)
        {
            var item = _posicionRepository.Get(id);
            if (item == null) return ActionConfirmation.CreateFailure("item no existe");

            try
            {
                _posicionRepository.Delete(item.Id);
                _posicionRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("Delete OK (" + item.Nombre + ")");
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(exception.Message,
                    exception.StackTrace, EventCategory.EliminarObjeto.ToString(), exception, item.ActualizadoPor, EventSource.Sistema);

                return ActionConfirmation.CreateFailure(exception.ToString());
            }
        }

        public PagedList<ImagenEvaluacion> GetImages(Guid evaluationId, int page = 0, int limit = 10)
        {
            return _imagenEvaluacionRepository.GetPagedListByEvaluacion(evaluationId, page, limit);
        }

        public ActionConfirmation CreateRespuestasByEvaluacion(Guid evaluacionId)
        {
            var evaluacion = Get(evaluacionId);
            if (evaluacion == null) return ActionConfirmation.CreateFailure("item no existe");

            try
            {
                var responsesToCreate = new List<Estandar>();

                var groups = _estandarService.GetGruposList();
                foreach (var grupoEstandar in groups)
                {
                    var headers = _estandarService.GetByGrupo(grupoEstandar.Id);
                    foreach (var header in headers)
                    {
                        var estandares = _estandarService.GetByParent(header.Id);
                        foreach (var estandar in estandares)
                        {
                            responsesToCreate.Add(estandar);

                            var picklist = _estandarService.GetPicklist(estandar.Id);
                            responsesToCreate.AddRange(picklist);

                            var subEstandares = _estandarService.GetByParent(estandar.Id);
                            foreach (var subEstandar in subEstandares)
                            {
                                responsesToCreate.Add(subEstandar);

                                var subpicklist = _estandarService.GetPicklist(subEstandar.Id);
                                responsesToCreate.AddRange(subpicklist);

                                var childEstandares = _estandarService.GetByParent(subEstandar.Id);
                                foreach (var childEstandar in childEstandares)
                                {
                                    responsesToCreate.Add(childEstandar);

                                    var childpicklist = _estandarService.GetPicklist(childEstandar.Id);
                                    responsesToCreate.AddRange(childpicklist);

                                    var subchildEstandares = _estandarService.GetByParent(childEstandar.Id);
                                    foreach (var subchildEstandar in subchildEstandares)
                                    {
                                        responsesToCreate.Add(subchildEstandar);

                                        var subchildpicklist = _estandarService.GetPicklist(subchildEstandar.Id);
                                        responsesToCreate.AddRange(subchildpicklist);
                                    }
                                }
                            }
                        }
                    }
                }

                // Create Responses
                var respCreated = new List<Respuesta>();
                if (responsesToCreate.Any())
                {
                    foreach (var estandar in responsesToCreate)
                    {
                        var respuesta = new Respuesta
                        {
                            Detalle = "",
                            FechaRespuesta = DateTime.Now,
                            CreadoPor = evaluacion.CreadoPor,
                            CreadoEn = DateTime.Now,
                            ActualizadoPor = evaluacion.ActualizadoPor,
                            ActualizadoEn = DateTime.Now,
                            Estandar = estandar,
                            Evaluacion = evaluacion,
                            Valor = (short)ValorRespuesta.Nulo
                        };


                        var respConfirmation = _respuestaService.SaveOrUpdate(respuesta);
                        if (respConfirmation.WasSuccessful)
                        {
                            if (estandar.TipoEstandar == (short)TipoEstandar.Estandar)
                            {
                                var dbResponse = _respuestaService.Get(respuesta.Id);
                                var comment = new RespuestaComentario
                                {
                                    Detalle = "",
                                    Valor = "",
                                    CreadoPor = evaluacion.CreadoPor,
                                    CreadoEn = DateTime.Now,
                                    ActualizadoPor = evaluacion.ActualizadoPor,
                                    ActualizadoEn = DateTime.Now,
                                    Respuesta = dbResponse
                                };

                                var commentConfirm = _respuestaService.SaveOrUpdateComentario(comment);
                            }

                            respCreated.Add(respuesta);
                        }

                    }
                }

                return ActionConfirmation.CreateSuccess($"respuestas creadas ({respCreated.Count})");
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(exception.Message,
                    exception.StackTrace, EventCategory.GuardarObjeto.ToString(), exception, evaluacion.ActualizadoPor, EventSource.Sistema);

                return ActionConfirmation.CreateFailure(exception.ToString());
            }
        }

        #endregion

    }
}