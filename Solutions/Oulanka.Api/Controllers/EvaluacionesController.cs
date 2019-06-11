using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Microsoft.Practices.ServiceLocation;
using Oulanka.Api.Models;
using Oulanka.Api.Models.FormModels;
using Oulanka.Api.Models.Services;
using Oulanka.Api.Models.ViewModels;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Dtos;
using Oulanka.Domain.Models.Evaluaciones;

namespace Oulanka.Api.Controllers
{
    [Route("api/[Controller]")]
    public class EvaluacionesController : BaseApiController
    {
        private readonly IEvaluacionService _evaluacionService;
        private readonly ILocalService _localService;
        private readonly IStatusService _statusService;
        private readonly IAuthorizationService _authorizationService;

        public EvaluacionesController()
        {
            _evaluacionService = ServiceLocator.Current.GetInstance<IEvaluacionService>();
            _localService = ServiceLocator.Current.GetInstance<ILocalService>();
            _statusService = ServiceLocator.Current.GetInstance<IStatusService>();
            _authorizationService = new AuthorizationService();
        }

        [Route("api/evaluaciones/{cadenaId}/{page}/{pageSize}")]
        [HttpGet]
        public async Task<IHttpActionResult> Get(Guid cadenaId, int? page = 1, int? pageSize = 10)
        {
            var pagedSet = new PaginationSet<EvaluacionDto>();
            try
            {
                if (await _authorizationService.AuthorizeAsync(User))
                {
                    var currentPage = page.Value;
                    var currentPageSize = pageSize.Value;

                    var evaluations = _evaluacionService.GetByCadenaPagedList(cadenaId, currentPage > 0 ? currentPage - 1 : currentPage, currentPageSize);
                    pagedSet = new PaginationSet<EvaluacionDto>
                    {
                        Page = currentPage,
                        TotalCount = (int)evaluations.TotalCount,
                        TotalPages = evaluations.TotalPages,
                        Items = evaluations.Items
                    };
                }
                else
                {
                    var codeResult = new CodeResultStatus(401);
                    return Ok(codeResult);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Ok(pagedSet);
        }


        [Route("api/evaluaciones/tipos")]
        [HttpGet]
        public async Task<IHttpActionResult> GetTiposEvaluacion()
        {
            IList<TipoEvaluacionDto> items = new List<TipoEvaluacionDto>();
            try
            {
                if (await _authorizationService.AuthorizeAsync(User))
                {
                    var tipos = _evaluacionService.GetTipoEvaluacionList(true);
                    foreach (var tipo in tipos)
                    {
                        items.Add(new TipoEvaluacionDto
                        {
                            Id = tipo.Id,
                            Nombre = tipo.Nombre,
                            ActualizadoPor = tipo.ActualizadoPor,
                            CreadoPor = tipo.CreadoPor,
                            NombreEstado = tipo.NombreEstado,
                            Descripcion = tipo.Descripcion,
                            ActualizadoEn = tipo.ActualizadoEn,
                            CreadoEn = tipo.CreadoEn,
                            IdEstado = tipo.Estado.Id,
                            Observaciones = tipo.Observaciones
                        });
                    }
                }
                else
                {
                    var codeResult = new CodeResultStatus(401);
                    return Ok(codeResult);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Ok(new { items });
        }

        [Route("api/evaluaciones/get/{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            EvaluacionDto item = null;
            try
            {
                if (await _authorizationService.AuthorizeAsync(User))
                {
                    var evaluacion = _evaluacionService.Get(id);
                    item = new EvaluacionDto
                    {
                        Id = evaluacion.Id,
                        ParteDelDia = evaluacion.ParteDelDia,
                        NombreParteDelDia = evaluacion.NombreParteDelDia,
                        ActualizadoPor = evaluacion.ActualizadoPor,
                        CreadoPor = evaluacion.CreadoPor,
                        LocalId = evaluacion.Local.Id,
                        LocalCodigo = evaluacion.Local.Codigo,
                        LocalNombre = evaluacion.Local.Nombre,
                        TipoLocalId = evaluacion.Local.TipoLocal.Id,
                        TipoLocalNombre = evaluacion.Local.TipoLocal.Detalle,
                        CadenaId = evaluacion.Local.Cadena.Id,
                        CadenaNombre = evaluacion.Local.Cadena.Nombre,
                        NombreRGM = evaluacion.NombreRGM,
                        NombreMIC = evaluacion.NombreMIC,
                        PosicionId = evaluacion.PosicionMIC.Id,
                        PosicionNombre = evaluacion.PosicionMIC.Nombre,
                        TipoVisita = evaluacion.TipoVisita,
                        TipoVisitaNombre = evaluacion.NombreTipoVisita,
                        Estado = evaluacion.NombreEstado,
                        EstadoId = evaluacion.Estado.Id,
                        TipoEvaluacionId = evaluacion.TipoEvaluacion.Id,
                        TipoEvaluacionNombre = evaluacion.TipoEvaluacion.Nombre,
                        ActualizadoEn = evaluacion.ActualizadoEn,
                        CreadoEn = evaluacion.CreadoEn
                    };
                }
                else
                {
                    var codeResult = new CodeResultStatus(401);
                    return Ok(codeResult);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Ok(item);
        }

        [Route("api/evaluaciones/create")]
        [HttpPost]
        public async Task<IHttpActionResult> Create([FromBody] EvaluacionFormModel formModel)
        {
            GenericResult itemResult = null;
            try
            {
                if (await _authorizationService.AuthorizeAsync(User))
                {
                    if (ModelState.IsValid)
                    {
                        var evaluacion = new Evaluacion
                        {
                            NombreMIC = formModel.NombreMIC,
                            NombreRGM = formModel.NombreRGM,
                            PosicionMIC = _evaluacionService.GetPosicion(formModel.Posicion),
                            TipoVisita = formModel.TipoVisita,
                            ParteDelDia = formModel.ParteDia,
                            TipoEvaluacion = _evaluacionService.GetTipoEvaluacion(formModel.TipoEvaluacion),
                            Local = _localService.Get(formModel.IdLocal),
                            Estado = _statusService.Online(),
                            FechaEvaluacion = DateTime.Now,
                            HoraEvaluacion = DateTime.Now,
                            ActualizadoEn = DateTime.Now,
                            CreadoEn = DateTime.Now,
                            ActualizadoPor = "admin",
                            CreadoPor = "admin",
                        };

                        var confirmation = _evaluacionService.SaveOrUpdate(evaluacion);
                        if (confirmation.WasSuccessful)
                        {
                           var respConfirmation = _evaluacionService.CreateRespuestasByEvaluacion(evaluacion.Id);

                            itemResult = GenericResult.Ok(confirmation.Message);
                            var item = confirmation.Value as Evaluacion;
                            itemResult.ReturnValue = new { Id = item.Id };
                        }
                        else
                        {
                            itemResult = GenericResult.Failure(confirmation.Message);
                        }
                    }
                }
                else
                {
                    var codeResult = new CodeResultStatus(401);
                    return Ok(codeResult);
                }
            }
            catch (System.Exception exception)
            {
                itemResult = GenericResult.Failure(exception.Message);
            }

            return Ok(itemResult);
        }
    }
}