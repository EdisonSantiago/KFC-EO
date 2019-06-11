using System;
using System.Collections.Generic;
using Oulanka.Domain.Common;
using Oulanka.Domain.Dtos;
using Oulanka.Domain.Models;
using Oulanka.Domain.Models.Evaluaciones;
using Oulanka.Domain.Models.Locales;
using Oulanka.Domain.Models.Personal;

namespace Oulanka.Domain.Contracts.Services
{
    public interface IEvaluacionService
    {
        PagedList<Evaluacion> GetPagedList(int page = 0, int limit = 10);
        PagedList<EvaluacionDto> GetByCadenaPagedList(Guid cadenaId, int page = 0, int limit = 10);
        PagedList<EvaluacionDto> GetByLocalPagedList(Guid localId, int page = 0, int limit = 10);
        IList<Evaluacion> GetList();
        Evaluacion Get(Guid id);
        ActionConfirmation SaveOrUpdate(Evaluacion evaluacion);
        ActionConfirmation Delete(Guid id);

        PagedList<TipoEvaluacion> GetTipoEvaluacionPagedList(int page = 0, int limit = 10);
        IList<TipoEvaluacion> GetTipoEvaluacionList();
        IList<TipoEvaluacion> GetTipoEvaluacionList(bool onlineOnly);
        TipoEvaluacion GetTipoEvaluacion(Guid id);
        ActionConfirmation SaveOrUpdateTipoEvaluacion(TipoEvaluacion tipoEquipo);
        ActionConfirmation DeleteTipoEvaluacion(Guid id);

        PagedList<Posicion> GetPosicionPagedList(int page = 0, int limit = 10);
        Posicion GetPosicion(Guid id);
        IList<Posicion> GetPosicionList();
        IList<Posicion> GetPosicionList(bool onlineOnly);
        IList<Posicion> GetPosicionList(Guid cadena, bool onlineOnly);
        ActionConfirmation SaveOrUpdatePosicion(Posicion posicion);
        ActionConfirmation DeletePosicion(Guid id);
        
        PagedList<ImagenEvaluacion> GetImages(Guid evaluationId, int i, int currentPageSize);
        ActionConfirmation CreateRespuestasByEvaluacion(Guid evaluacionId);
    }
}