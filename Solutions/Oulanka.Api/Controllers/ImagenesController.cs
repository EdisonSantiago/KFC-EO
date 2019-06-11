using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Microsoft.Practices.ServiceLocation;
using Oulanka.Api.Models;
using Oulanka.Api.Models.FormModels;
using Oulanka.Api.Models.Services;
using Oulanka.Domain;
using Oulanka.Domain.Common;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Dtos;
using Oulanka.Domain.Models.Evaluaciones;
using Oulanka.Domain.Models.Locales;

namespace Oulanka.Api.Controllers
{
    [System.Web.Mvc.Route("api/[Controller]")]
    public class ImagenesController : BaseApiController
    {
        private readonly IEvaluacionService _evaluacionService;
        private readonly ILocalService _localService;
        private readonly IStatusService _statusService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IBlobImageService _blobImageService;

        public ImagenesController()
        {
            _blobImageService = ServiceLocator.Current.GetInstance<IBlobImageService>();
            _evaluacionService = ServiceLocator.Current.GetInstance<IEvaluacionService>();
            _localService = ServiceLocator.Current.GetInstance<ILocalService>();
            _statusService = ServiceLocator.Current.GetInstance<IStatusService>();
            _authorizationService = new AuthorizationService();
        }

        [Route("api/imagenes/evaluacion/{evaluationId}/{page}/{pageSize}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetByEvaluation(Guid evaluationId, int? page = 1, int? pageSize = 10)
        {
            var pagedSet = new PaginationSet<ImagenEvaluacionDto>();
            try
            {
                if (await _authorizationService.AuthorizeAsync(User))
                {
                    var currentPage = page.Value;
                    var currentPageSize = pageSize.Value;

                    var dbEvaluations = _evaluacionService.GetImages(evaluationId, currentPage > 0 ? currentPage - 1 : currentPage, currentPageSize);
                    var evaluations = Mapper.Map<PagedList<ImagenEvaluacion>, PagedList<ImagenEvaluacionDto>>(dbEvaluations);

                    pagedSet = new PaginationSet<ImagenEvaluacionDto>
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

        [Route("api/imagenes/local/{localId}/{page}/{pageSize}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetByLocal(Guid localId, int? page = 1, int? pageSize = 10)
        {
            var pagedSet = new PaginationSet<ImagenLocalDto>();
            try
            {
                if (await _authorizationService.AuthorizeAsync(User))
                {
                    var currentPage = page.Value;
                    var currentPageSize = pageSize.Value;

                    var dbEvaluations = _localService.GetImages(localId, currentPage > 0 ? currentPage - 1 : currentPage, currentPageSize);
                    var evaluations = Mapper.Map<PagedList<ImagenLocal>, PagedList<ImagenLocalDto>>(dbEvaluations);

                    pagedSet = new PaginationSet<ImagenLocalDto>
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

        [Route("api/imagenes/localsave")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveByLocal([FromBody] ImagenLocalFormModel formModel)
        {
            GenericResult itemResult = null;
            try
            {
                if (await _authorizationService.AuthorizeAsync(User))
                {
                    var imagenLocal = _localService.GetImage(formModel.Id) ?? new ImagenLocal();

                    if (imagenLocal.Id == Guid.Empty)
                    {
                        imagenLocal.CreadoEn = DateTime.Now;
                        imagenLocal.CreadoPor = User.Identity.Name;
                    }

                    imagenLocal.ActualizadoEn = DateTime.Now;
                    imagenLocal.ActualizadoPor = User.Identity.Name;
                    imagenLocal.Descripcion = !string.IsNullOrEmpty(formModel.Descripcion) ? formModel.Descripcion : "";
                    imagenLocal.Tipo = formModel.Tipo;
                    imagenLocal.Orden = formModel.Orden;

                    var local = _localService.Get(formModel.LocalId);
                    imagenLocal.Local = local;

                    if (!string.IsNullOrEmpty(formModel.ImagenData))
                    {
                        var imageBase64 = formModel.ImagenData.Replace("data:image/jpeg;base64,", "");
                        var imageBytes = Convert.FromBase64String(imageBase64);
                        var imageUrl = _blobImageService.UploadImage(imageBytes, BlobContainers.Locales());
                        imagenLocal.Imagen = imageUrl;
                    }

                    ActionConfirmation confirmation = _localService.SaveOrUpdateImagen(imagenLocal);
                    if (confirmation.WasSuccessful)
                    {
                        itemResult = GenericResult.Ok(confirmation.Message);
                        var item = confirmation.Value as ImagenLocal;
                        itemResult.ReturnValue = new { Id = item.Id };
                    }
                    else
                    {
                        itemResult = GenericResult.Failure(confirmation.Message);
                    }
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