using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Practices.ServiceLocation;
using Oulanka.Api.Models;
using Oulanka.Api.Models.Services;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Dtos;
using Oulanka.Domain.Models;

namespace Oulanka.Api.Controllers
{
    [Route("api/[Controller]")]
    public class CadenasController : BaseApiController
    {
        private readonly ICadenaService _cadenaService;
        private readonly IAuthorizationService _authorizationService;

        public CadenasController()
        {
            _cadenaService = ServiceLocator.Current.GetInstance<ICadenaService>();
            _authorizationService = new AuthorizationService();
        }

        public async Task<IHttpActionResult> Get(int? page, int? pageSize)
        {
            var items = new PagedList<Cadena>();
            try
            {
                if (await _authorizationService.AuthorizeAsync(User))
                {
                    var currentPage = page.Value;
                    var currentPageSize = pageSize.Value;

                    items = _cadenaService.GetPagedList(currentPage, currentPageSize);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Ok(new { items });
        }

        [Route("api/cadenas/list")]
        [HttpGet]
        public async Task<IHttpActionResult> GetList()
        {
            var items = new List<CadenaDto>();
            try
            {
                if (await _authorizationService.AuthorizeAsync(User))
                {
                    var dbItems = _cadenaService.GetList(true);

                    foreach (var cadena in dbItems)
                    {
                        items.Add(
                            new CadenaDto
                            {
                                Id = cadena.Id,
                                Manual = cadena.Manual,
                                Logo = cadena.Logo,
                                Nombre = cadena.Nombre,
                                NombreEstado = cadena.NombreEstado,
                                ActualizadoEn = cadena.ActualizadoEn,
                                ActualizadoPor = cadena.ActualizadoPor,
                                CreadoEn = cadena.CreadoEn,
                                CreadoPor = cadena.CreadoPor,
                                Descripcion = cadena.Descripcion,
                                EstadoId = cadena.Estado.Id,
                                FechaFundacion = cadena.FechaFundacion
                            });
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Ok(new { items = items });
        }


        public async Task<IHttpActionResult> GetById(Guid id)
        {
            Cadena item = null;
            try
            {
                if (await _authorizationService.AuthorizeAsync(User))
                {
                    item = _cadenaService.Get(id);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Ok(item);
        }
    }
}