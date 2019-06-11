using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Microsoft.Practices.ServiceLocation;
using NHibernate.Mapping;
using NHibernate.Util;
using Oulanka.Api.Models;
using Oulanka.Api.Models.Services;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Dtos;
using Oulanka.Domain.Enums;
using Oulanka.Domain.Models.Estandares;

namespace Oulanka.Api.Controllers
{
    [System.Web.Mvc.Route("api/[Controller]")]
    public class EstandaresController : BaseApiController
    {
        private readonly IEstandarService _estandarService;
        private readonly IStatusService _statusService;
        private readonly IAuthorizationService _authorizationService;

        public EstandaresController()
        {
            _estandarService = ServiceLocator.Current.GetInstance<IEstandarService>();
            _statusService = ServiceLocator.Current.GetInstance<IStatusService>();
            _authorizationService = new AuthorizationService();
        }


        [Route("api/estandares/getbyparent/{parentId}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetByParent(Guid parentId)
        {
            IList<EstandarDto> items;
            try
            {
                if (await _authorizationService.AuthorizeAsync(User))
                {
                    var dbItems = _estandarService.GetByParent(parentId);
                    items = Mapper.Map<IList<Estandar>, IList<EstandarDto>>(dbItems);
                }
                else
                {
                    var codeResult = new CodeResultStatus(401);
                    return Ok(codeResult);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }

            return Ok(items);
        }


        [Route("api/estandares/getbyid/{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetById(Guid id)
        {
            EstandarDto item;
            try
            {
                if (await _authorizationService.AuthorizeAsync(User))
                {
                    var dbItem = _estandarService.Get(id);
                    item = Mapper.Map<Estandar,EstandarDto>(dbItem);
                }
                else
                {
                    var codeResult = new CodeResultStatus(401);
                    return Ok(codeResult);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }

            return Ok(item);
        }

        [Route("api/estandares/picklist/{parentId}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetPicklist(Guid parentId)
        {
            IList<EstandarDto> items;
            try
            {
                if (await _authorizationService.AuthorizeAsync(User))
                {
                    var dbItems = _estandarService.GetPicklist(parentId);
                    items = Mapper.Map<IList<Estandar>, IList<EstandarDto>>(dbItems);
                }
                else
                {
                    var codeResult = new CodeResultStatus(401);
                    return Ok(codeResult);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }

            return Ok(items);
        }

        [Route("api/estandares/getbygroup/{groupId}/{standardType}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetByGroup(Guid groupId, short standardType = 1)
        {
            IList<EstandarDto> items;
            try
            {
                if (await _authorizationService.AuthorizeAsync(User))
                {
                    var tipoEstandar = (TipoEstandar)Enum.Parse(typeof(TipoEstandar), standardType.ToString());
                    var dbItems = _estandarService.GetByGrupo(groupId, tipoEstandar);
                    items = Mapper.Map<IList<Estandar>, IList<EstandarDto>>(dbItems);
                }
                else
                {
                    var codeResult = new CodeResultStatus(401);
                    return Ok(codeResult);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }

            return Ok(items);
        }


        // Grupo Estandares
        [Route("api/estandares/grupos")]
        [HttpGet]
        public async Task<IHttpActionResult> GetGroups()
        {
            IList<GrupoEstandarDto> items;
            try
            {
                if (await _authorizationService.AuthorizeAsync(User))
                {
                    var dbItems = _estandarService.GetGruposList();
                    items = new List<GrupoEstandarDto>();

                    foreach (var grupo in dbItems)
                    {
                        var dto = new GrupoEstandarDto
                        {
                            Id = grupo.Id,
                            Codigo = grupo.Codigo,
                            Nombre = grupo.Nombre,
                            Descripcion = grupo.Descripcion,
                            Imagen = grupo.Imagen,
                            CreadoPor = grupo.CreadoPor,
                            ActualizadoPor = grupo.ActualizadoPor,
                            CreadoEn = grupo.CreadoEn,
                            ActualizadoEn = grupo.ActualizadoEn,
                            EstadoId = grupo.Estado.Id,
                            EstadoNombre = grupo.Estado.Nombre,
                        };
                        items.Add(dto);
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

            return Ok(items);
        }

    }
}