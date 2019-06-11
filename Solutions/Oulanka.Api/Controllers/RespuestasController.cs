using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Microsoft.Practices.ServiceLocation;
using Oulanka.Api.Models;
using Oulanka.Api.Models.FormModels;
using Oulanka.Api.Models.Services;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Dtos;
using Oulanka.Domain.Models.Respuestas;

namespace Oulanka.Api.Controllers
{
    [System.Web.Mvc.Route("api/[Controller]")]
    public class RespuestasController : BaseApiController
    {
        private readonly IEvaluacionService _evaluacionService;
        private readonly IEstandarService _estandarService;
        private readonly IRespuestaService _respuestaService;
        private readonly IStatusService _statusService;
        private readonly IAuthorizationService _authorizationService;

        public RespuestasController()
        {
            _evaluacionService = ServiceLocator.Current.GetInstance<IEvaluacionService>();
            _estandarService = ServiceLocator.Current.GetInstance<IEstandarService>();
            _respuestaService = ServiceLocator.Current.GetInstance<IRespuestaService>();
            _statusService = ServiceLocator.Current.GetInstance<IStatusService>();
            _authorizationService = new AuthorizationService();
        }

        [Route("api/respuestas/getbyeval/{evaluacionId}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetByEvaluacion(Guid evaluacionId)
        {
            IList<RespuestaDto> items;
            try
            {
                if (await _authorizationService.AuthorizeAsync(User))
                {
                    var respuestas = _respuestaService.GetListByEvaluacion(evaluacionId);
                    items = Mapper.Map<IList<Respuesta>, IList<RespuestaDto>>(respuestas);
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

            return Ok(items);
        }

        [Route("api/respuestas/getcomentariosbyeval/{evaluacionId}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetComentariosByEvaluacion(Guid evaluacionId)
        {
            IList<RespuestaComentarioDto> items;
            try
            {
                if (await _authorizationService.AuthorizeAsync(User))
                {
                    var comentarios = _respuestaService.GetCommentariosListByEvaluacion(evaluacionId);
                    items = Mapper.Map<IList<RespuestaComentario>, IList<RespuestaComentarioDto>>(comentarios);
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

            return Ok(items);
        }

        [Route("api/respuestas/save")]
        [HttpPost]
        public async Task<IHttpActionResult> Save([FromBody] RespuestaFormModel formModel)
        {
            GenericResult itemResult = null;
            try
            {
                if (await _authorizationService.AuthorizeAsync(User))
                {
                    if (ModelState.IsValid)
                    {
                        var respuesta = _respuestaService.Get(formModel.Id) ?? new Respuesta();

                        respuesta.Valor = formModel.Valor;
                        respuesta.Detalle = formModel.Detalle;
                        respuesta.FechaRespuesta = formModel.FechaRespuesta;

                        if (respuesta.Id == Guid.Empty)
                        {
                            respuesta.CreadoPor = User.Identity.Name;
                            respuesta.CreadoEn = DateTime.Now;
                        }

                        respuesta.ActualizadoPor = User.Identity.Name;
                        respuesta.ActualizadoEn = DateTime.Now;

                        var estandar = _estandarService.Get(formModel.EstandarId);
                        respuesta.Estandar = estandar;

                        var evaluacion = _evaluacionService.Get(formModel.EvaluacionId);
                        respuesta.Evaluacion = evaluacion;

                        var confirmation = _respuestaService.SaveOrUpdate(respuesta);
                        if (confirmation.WasSuccessful)
                        {
                            itemResult = GenericResult.Ok(confirmation.Message);
                            var item = confirmation.Value as Respuesta;
                            itemResult.ReturnValue = new {Id = item.Id};
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
            catch (Exception exception)
            {
                itemResult = GenericResult.Failure(exception.Message);
            }

            return Ok(itemResult);
        }

    }
}