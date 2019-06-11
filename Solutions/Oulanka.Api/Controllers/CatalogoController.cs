using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Microsoft.Practices.ServiceLocation;
using Oulanka.Api.Models;
using Oulanka.Api.Models.Services;
using Oulanka.Api.Models.ViewModels;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Dtos;
using Oulanka.Domain.Enums;
using Oulanka.Domain.Models.Personal;

namespace Oulanka.Api.Controllers
{
    [Route("api/[Controller]")]
    public class CatalogoController : BaseApiController
    {
        private readonly IEvaluacionService _evaluacionService;
        private readonly IAuthorizationService _authorizationService;

        public CatalogoController()
        {
            _evaluacionService = ServiceLocator.Current.GetInstance<IEvaluacionService>();
            _authorizationService = new AuthorizationService();
        }

        [Route("api/catalogo/get/{cadena}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCatalogo(Guid cadena)
        {
            CatalogoViewModel catalogo = null;

            try
            {
                if (await _authorizationService.AuthorizeAsync(User))
                {
                    var dbPosiciones = _evaluacionService.GetPosicionList(cadena, true);
                    var posiciones = new List<PosicionDto>();
                    foreach (var posicion in dbPosiciones)
                    {
                        posiciones.Add(new PosicionDto
                        {
                            Id = posicion.Id,
                            Nombre = posicion.Nombre,
                            ActualizadoPor = posicion.ActualizadoPor,
                            CreadoPor = posicion.CreadoPor,
                            NombreEstado = posicion.Estado.Nombre,
                            Descripcion = posicion.Descripcion,
                            ActualizadoEn = posicion.ActualizadoEn,
                            CreadoEn = posicion.CreadoEn,
                            IdCadena = posicion.Cadena.Id,
                            IdEstado = posicion.Estado.Id,
                            NombreCadena = posicion.Cadena.Nombre
                        });
                    }

                    var partesDia = GetPartesDiaList();
                    var tiposVisita = GetTipoVisitaList();

                    catalogo = new CatalogoViewModel
                    {
                        Posiciones = posiciones,
                        PartesDia = partesDia,
                        TiposVisita = tiposVisita
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

            return Ok(catalogo);

        }

        [Route("api/catalogo/posiciones")]
        [HttpGet]
        public async Task<IHttpActionResult> GetPosiciones()
        {
            IList<PosicionDto> items = new List<PosicionDto>();
            try
            {
                if (await _authorizationService.AuthorizeAsync(User))
                {
                    var posiciones = _evaluacionService.GetPosicionList(true);

                    foreach (var posicion in posiciones)
                    {
                        items.Add(new PosicionDto
                        {
                            Id = posicion.Id,
                            Nombre = posicion.Nombre,
                            ActualizadoPor = posicion.ActualizadoPor,
                            CreadoPor = posicion.CreadoPor,
                            NombreEstado = posicion.Estado.Nombre,
                            Descripcion = posicion.Descripcion,
                            ActualizadoEn = posicion.ActualizadoEn,
                            CreadoEn = posicion.CreadoEn,
                            IdCadena = posicion.Cadena.Id,
                            IdEstado = posicion.Estado.Id,
                            NombreCadena = posicion.Cadena.Nombre
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

        [Route("api/catalogo/posiciones/{cadena}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetPosicionesByCadena(Guid cadena)
        {
            IList<PosicionDto> items = new List<PosicionDto>();
            try
            {
                if (await _authorizationService.AuthorizeAsync(User))
                {
                    var posiciones = _evaluacionService.GetPosicionList(cadena, true);
                    foreach (var posicion in posiciones)
                    {
                        items.Add(new PosicionDto
                        {
                            Id = posicion.Id,
                            Nombre = posicion.Nombre,
                            ActualizadoPor = posicion.ActualizadoPor,
                            CreadoPor = posicion.CreadoPor,
                            NombreEstado = posicion.Estado.Nombre,
                            Descripcion = posicion.Descripcion,
                            ActualizadoEn = posicion.ActualizadoEn,
                            CreadoEn = posicion.CreadoEn,
                            IdCadena = posicion.Cadena.Id,
                            IdEstado = posicion.Estado.Id,
                            NombreCadena = posicion.Cadena.Nombre
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

        [Route("api/catalogo/tiposvisita")]
        [HttpGet]
        public async Task<IHttpActionResult> GetTiposVisita()
        {
            var items = GetTipoVisitaList();

            return Ok(new { items });
        }

        [Route("api/catalogo/partesdia")]
        [HttpGet]
        public async Task<IHttpActionResult> GetPartesDia()
        {
            var items = GetPartesDiaList();

            return Ok(new { items });
        }

        private IList<NameValue> GetTipoVisitaList()
        {
            var items = Enum.GetValues(typeof(TipoVisita))
                .Cast<TipoVisita>()
                .Select(v => new NameValue { Id = (int)v, Nombre = v.ToString() }).ToList();

            return (items);
        }

        private IList<NameValue> GetPartesDiaList()
        {
            var items = Enum.GetValues(typeof(ParteDelDia))
                .Cast<ParteDelDia>()
                .Select(v => new NameValue { Id = (int)v, Nombre = v.ToString() }).ToList();

            return (items);
        }
    }
}