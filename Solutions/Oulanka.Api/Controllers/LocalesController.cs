using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Microsoft.Practices.ServiceLocation;
using Oulanka.Api.Models;
using Oulanka.Api.Models.Services;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Dtos;
using Oulanka.Domain.Models.Locales;

namespace Oulanka.Api.Controllers
{
    [Route("api/[Controller]")]
    public class LocalesController : BaseApiController
    {
        private readonly ILocalService _localService;
        private readonly IAuthorizationService _authorizationService;

        public LocalesController()
        {
            _localService = ServiceLocator.Current.GetInstance<ILocalService>();
            _authorizationService = new AuthorizationService();
        }

        [Route("api/locales/{cadenaId}/{page}/{pageSize}")]
        [HttpGet]
        public async Task<IHttpActionResult> Get(Guid cadenaId, int? page = 1, int? pageSize = 10)
        {
            var pagedSet = new PaginationSet<LocalDto>();
            try
            {
                if (await _authorizationService.AuthorizeAsync(User))
                {
                    var currentPage = page.Value;
                    var currentPageSize = pageSize.Value;

                    var locales = _localService.GetPagedList(cadenaId, currentPage > 0 ? currentPage - 1 : currentPage, currentPageSize);
                    var localesDto = Mapper.Map<IEnumerable<Local>,IEnumerable<LocalDto >>(locales.Items);
                    
                    pagedSet = new PaginationSet<LocalDto>
                    {
                        Page = currentPage,
                        TotalCount = (int)locales.TotalCount,
                        TotalPages = locales.TotalPages,
                        Items = localesDto
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

        [Route("api/locales/list/{cadena}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetList(Guid cadena)
        {
            var items = new List<LocalDto>();
            try
            {
                if (await _authorizationService.AuthorizeAsync(User))
                {
                   var locales = _localService.GetList(cadena, true);
                    items = Mapper.Map<IEnumerable<Local>, List<LocalDto>>(locales);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Ok(new {items});
        }

        [Route("api/locales/get/{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetById(Guid id)
        {
            LocalDto item = null;
            try
            {
                if (await _authorizationService.AuthorizeAsync(User))
                {
                    var local = _localService.Get(id);
                    item = Mapper.Map<Local, LocalDto>(local);
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

        [Route("api/locales/getbycode/{code}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetById(string code)
        {
            LocalDto item = null;
            try
            {
                if (await _authorizationService.AuthorizeAsync(User))
                {
                    var local = _localService.GetByCode(code);
                    item = Mapper.Map<Local, LocalDto>(local);
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
    }
}